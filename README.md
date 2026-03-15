Online retail platform for a client operating in the global market with millions of daily users.

Prerequisites: Have Docker installed

In Powershell navigate to the project directory and execute:
docker-compose up --build

In browser open http://localhost:5000/swagger/index.html

Execute the POST api/Auth/token method.
You will get a token.

After that you can execute the other method, by first clicking on the Authorize button on top right and entering the token from the previous step in the dialog.