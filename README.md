# SMS_Example_Survey


### Requirements
Ngrok https://ngrok.com/download

Visual Studio https://www.visualstudio.com/


### Setup
1. Navigate to the downloaded ngrok directory in command prompt and run: ngrok http 5000  
2. Open the project solution in Visual Studio by opening the .csproj file  
3. In the Startup.cs file, Enter your Bandwidth account credentials  
      i. If you don't have these, visit https://catapult.inetwork.com to create an account, and get credentials from the account tab  
4. Run the project by selecting SMS_Example_Survey from the run menu (right beside the green arrow)  
5. In your web browser, navigate to the ngrok forwarding URL received in step 1 (ex. https://12345678.ngrok.io)
      i. When this step is completed, you should see a page that contains a phone number for the application.  
6. Navigate to https://catapult.inetwork.com and record application information  
      i. After signing in, go to the Applications tab  
      ii. Select the application created for the sample app  
      iii. Record the Messaging callback URL and the phone number assigned to the application.  
7. In Visual Studio, go to FinishStartup.cs, and enter your account credentials the information from step 6.  
8. Run the project again, and you are ready to go!  



### Using the application
To use the application GUI, navigate to /surveys in your web browser (ex. https://12345678.ngrok.io/surveys). You can also use GET and POST requests to interact with the application, using Postman or a similar service. A guide to GET and POST requests is posted in the Wiki for this repository.





