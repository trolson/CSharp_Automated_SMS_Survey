# SMS_Example_Survey

Run with Ngrok. https://ngrok.com/download

Once downloaded, navigate to the downloaded ngrok directory in command prompt and run: ngrok http 5000 


## Run

Open the project solution in Visual Studio. Enter in your bandwidth catapult account credentials in the FinishStartup.cs file.
Run the program by starting the web server (press the green run button in Visual Studio). Next, navigate to the ngrok url
you got. This will be a get request that builds the application on your catapult account. Navigate to the catapult dashboard and
go to your apps. Grab the callback url and phone number that was assigned and enter them in for callbackurl and from number
in the RunAsync method in FinishStartup.cs

Now, run the program again in Visual Studio. You can then navigate to the browser with the web app running 
or use Postman (recommended) to perform get and post requests to the application. You can post to create a new survey,
add questions to a survey, and add numbers to the survey that will send out the survey to the numbers posted. 
Get requests can be performed to view all the surveys (name and id) as well as viewing the results of a single survey.

A full guide for the get and post requests using postman can be found on the wiki.

