# XChangeAPI

This is the XChange currency converter, a demo project meant as an interview test for Brian Joseph Pace.

Source code is hosted on github: 

To build the application, you must have Docker with Docker Compose installed.

1) Open a terminal window at 'Docker/', modify the .env file and run 'docker compose up -d' in that specific location. This will generate redis, mariadb and adminer containers.

2) Go back to the root directory/XChangeAPI/XChangeAPI and fill in the details inside the appsettings. There are three locations to modify:
	a) APIToken: User-specific API token provided by fixer.io
	b) JWTToken: A random string of characters to uniquely identify the application login token.
	c) sqlConnString: Fill in your username and password that was entered in step 1 in the .env file.

3) Go back a directory level and run 'docker compose up -d' in that specific location. This should build and deploy a containerised application.


To run the application, kindly follow these steps.

4) Navigate to http://localhost:8083/swagger/index.html
5) In the User section, register yourself by invoking the endpoint. The response should contain a bearer token and your user ID. Take note of both.
6) Near the top of the page, there is a green Authorise button. Click it and paste the token and submit.
7) You may now proceed to invoke the exchange rate API by navigating to the Ticker section and invoke "Exchange". Supply the body with the structure of the example and test.
8) The bearer token lasts a single day. To refresh the token, invoke the Login call in the User section using your username and password.
