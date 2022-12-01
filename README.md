# Lunch Camera

In my day job we have a canteen. With a lot of people working for teh compnay the queue can often be 30 minutes long. That is half of lunch break and I could be writing code instead of queueing for lunch. We have a camera that looks down teh corridor leading to the canteen. I worked out it gave us one picture every 5 minutes which it puts on a server. All I did is write this app to pick up that picture and display it. I did start with a Web browser but found it better to use a picturebox. I left the browser in just in case someone finds it useful.

In the Form1.cs search for GetResponseStream. It is in two places and you will find it commented out. Uncomment it, add the link to your picture, coment out the globe.jpeg and you are set to run.     
