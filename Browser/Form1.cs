using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Browser.Properties;

namespace Browser
{
    public partial class Form1 : Form
    {
    	private bool flag = true;
    	Thread thread;
    	
        public Form1()
        {
            InitializeComponent();
        }

        void Form1Load(object sender, EventArgs e)
        {
            if (File.Exists(@"globe.jpeg")) File.Delete(@"globe.jpeg");

            Bitmap bmpGlobe = Resources.globe; //get the resource

            using (var fs = new FileStream("globe.jpeg", FileMode.Create, FileAccess.Write))
            {
                bmpGlobe.Save(fs, ImageFormat.Jpeg); //save the resource to file
            }


            Text += " : v" + Assembly.GetExecutingAssembly().GetName().Version; // put in the version number

            thread = new Thread(new ThreadStart(WorkThreadFunction));
            thread.Start();
        }


        void btn_go_Click(object sender, EventArgs e)
        {
        	webBrowser1.Navigate(txtbx_url.Text.Trim());
        }
        
         void btn_stop_Click(object sender, System.EventArgs e)
        {
        	webBrowser1.Stop();
        }
                 
        void btn_refresh_Click(object sender, EventArgs e)
        {
        	webBrowser1.Refresh();
        }
        
        void btn_forward_Click(object sender, EventArgs e)
        {
        	webBrowser1.GoForward();
        }
        
        void btn_back_Click(object sender, EventArgs e)
        {
        	if (webBrowser1.CanGoBack)
            {

                webBrowser1.GoBack();
            }
        }
        
        void btn_home_Click(object sender, EventArgs e)
        {
        	webBrowser1.GoHome();
        }
        
        void Btn_closeClick(object sender, EventArgs e)
        {
        	Close();
        }
        
        void Btn_cameraClick(object sender, EventArgs e)
        {
            //Uncomment if you are getting the picture from the web into the browser
            //Change the URL to point to your image below is just for illustration it will not work.

            //webBrowser1.Navigate("https://lunchcam.com/images/canteen.jpeg");
            //txtbx_url.Text = "https://lunchcam.com/images/canteen.jpeg";

        }
        
       
	   public void WorkThreadFunction()
		{
		  try
		  {
		  	while (true)
		  	{
		  		if (flag)
		  		{		    		
		  			GetPicture();
					Thread.Sleep(300000); //5min update
		  		}
		  		else
		  		{
		  			if (picbx_camera.Image != null) picbx_camera.Image = null;
		  			Thread.Sleep(60000); //1 min update
		  		}
				
		  	}
		  }
		  catch (Exception ex)
		  {
		  	//for the moment ignore the exceptions.
		  	//MessageBox.Show(ex.ToString());
		  }
		}
             
        void Form1FormClosing(object sender, FormClosingEventArgs e)
        {
        	thread.Abort(); //close the open thread
        }
        
        void Btn_pic_refreshClick(object sender, EventArgs e)
        {
        	if (picbx_camera.Image != null)
        	{
        		GetPicture();
        		flag = true;
        		btn_stop_picture.Text = "Stop";
        	}
        }
        
        void Btn_stop_pictureClick(object sender, EventArgs e)
        {
        	if (btn_stop_picture.Text=="Stop")
        	{
        	  	btn_stop_picture.Text = "Start";
        		flag = false; 
        		lbl_last_update.Text = "";
        		picbx_camera.Image = null;
        		btn_pic_refresh.Visible = false;
        	}
        	else
        	{
        		btn_stop_picture.Text = "Stop";
        		GetPicture();
        		flag = true;
        	}
        }
        
        void GetPicture()
        {
            /*
            //Camera is on https but WeRequest only likes http
            WebRequest request = WebRequest.Create("http://lunchcam.com/images/canteen.jpeg");

            using (var response = request.GetResponse())
			using (var stream = response.GetResponseStream())
			{				
				picbx_camera.Image = Bitmap.FromStream(stream);
			}*/
            ///////////////////////////////////////////////////////////////////////////

            picbx_camera.Image = Image.FromFile("globe.jpeg"); //comment out if using part above

            //Invoke if not on this thread
            if (btn_pic_refresh.InvokeRequired)
			{
				btn_pic_refresh.BeginInvoke((MethodInvoker) delegate() { btn_pic_refresh.Visible = true;});
			
			}
			else
			{
				btn_pic_refresh.Visible = true;
			}
			
			//Invoke if not on this thread
			if(lbl_last_update.InvokeRequired)
			{
				lbl_last_update.BeginInvoke((MethodInvoker) delegate() {lbl_last_update.Text = "Last Updated: " + DateTime.Now;});
			}
			else
			{
				lbl_last_update.Text = "Last Updated: " + DateTime.Now;
			}
        }
    }
}
