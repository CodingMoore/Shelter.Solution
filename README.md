# **Shelter.Solution**
Project Initiated: 2021-01-23<br>
Updated: 2021-01-24

## **Project Description**

This project is an exercise in creating an ASP.NET Core API with database integration using multiple classes and Full CRUD implementation.  This project also includes CORS integration, Swagger Documentation, and JSON Web Token (JWT) authentication for Create, Update, and Delete functionality.

Coding Prompt:

Create an API for an animal shelter. The API will list the available cats and dogs at the shelter.

You have the freedom to build out your API as you wish. At the very least, your API should include the following:

* Full CRUD functionality.
* Further exploration of one of the following objectives: authentication, versioning, pagination, Swagger documentation, or CORS.
* Complete documentation of API endpoints and the further exploration you did.

## **Required for Use**
* C# and .Net Core installed on your local machine. (Developed on .Net Core v2.2.4)
* Console/Terminal access.
* Code Editor like [Visual Studio Code](https://code.visualstudio.com/)
* MySQL Community Server
* MySQL Workbench
* Postman (for JWT authentication)


## **Installation Instructions**
Download [.Net Core](https://dotnet.microsoft.com/download) and follow the installation instructions for your computer's
operating system.



### **Installing C#, .NET**
Install C# and .Net according to your operating system below:

**For Mac**
1. Download this .NET Core SDK Software Development Kit. 
2. Open the .pkg file. This will launch an installer which will walk you through installation steps. Use the default settings the installer suggests.
3. Confirm the installation is successful by opening your terminal and running the command <code>dotnet --version</code>, which should return the correct version number.

**For Windows (10+)**
1. Download either the the 64-bit .NET Core SDK Software Development Kit
2. Open the file and follow the steps provided by the installer for your OS.
3. Confirm the installation is successful by opening a new Windows PowerShell window and running the command <code>dotnet --version</code> You should see a response with the correct version number.

**My SQL Installation Below** 

**Windows 10 -**

Start by downloading the MySQL Web Installer from the [MySQL Downloads](https://dev.mysql.com/downloads/file/?id=484919) page. (Use the No thanks, just start my download link.)

Follow along with the installer:

1) Click "Yes" if prompted to update.
2) Accept license terms.
3) Choose Custom setup type.
4) When prompted to Select Products and Features, choose the following:
* MySQL Server 8.0.19 (This will be under "MySQL Servers > MySQL Server > MySQL Server 8.0")

* MySQL Workbench 8.0.19 (This will be under "Applications > MySQL Workbench > MySQL Workbench 8.0")

5) Select "Next", then "Execute". Wait for download and installation. (This can take a few minutes.)

6) Advance through Configuration as follows:

* High Availability set to "Standalone".

* "Defaults are OK" under Type and Networking.

* Authentication Method set to Use Legacy Authentication Method.

* Set password to something you will remember. 

* Defaults are OK under Windows Service. Make sure that checkboxes are checked for the options "Configure MySQL Server as a Windows Service" and "Start the MySQL Server at System Startup". Under Run Windows Service as..., the "Standard System Account" should be selected.

7) Complete Installation process.
Next, add the MySQL environment variable to the System PATH. We must include MySQL in the System Environment Path Variable. This is its own multi-step process:

8) Open the Control Panel and visit System and "Security > System". Select "Change Settings" and a pop-up window will display. Select the tab "Advanced" and select the "Environment Variables" button.
9) Within the System Variables navigator window, select PATH..., click Edit..., and then New.

10) Add the exact location of your MySQL installation, and click OK. (This location is likely C:\Program Files\MySQL\MySQL Server 8.0\bin, but may differ depending on your specific installation.)

**MacOS -**

Start by downloading the MySQL Community Server .dmg file from the [MySQL Community Server page](https://dev.mysql.com/downloads/file/?id=484914). Click the download icon. Use the No thanks, just start my download link.

Next, follow along with the Installer until you reach the Configuration page. Once you've reached Configuration, select or set the following options (use default if not specified):

1) Use Legacy Password Encryption.

2) Set password to something you will remember.

3) Click Finish.

4) Open the terminal and enter the command echo 'export PATH="/usr/local/mysql/bin:$PATH"' >> ~/.bash_profile. This will save this path in .bash_profile, which is where our terminal is configured.

5) Type in source ~/.bash_profile (or restart the terminal) in order to actually verify that MySQL was installed.

Next, verify MySQL installation by opening terminal and entering the command mysql -uroot -pepicodus. You'll know it's working and connected if you gain access and see the MySQL command line. If it's not working, you'll likely get a -bash: mysql: command not found error.

You can exit the mysql program by entering exit.

Next, download the MySQL Workbench .dmg file from the MySQL Workbench page. (Use the No thanks, just start my download link.)

Install MySQL Workbench to Applications folder.

Then open MySQL Workbench and select the Local instance 3306 server. You will need to enter the password you set. (We used epicodus.) If it connects, you're all set.


### **Install/Setup Project** ###

**Option 1** (download zip file)
1) Copy and paste the following GitHub project link into your web browser's url bar and hit enter/return. https://github.com/RMGit-it/Shelter.Solution.git
2) Download a .zip copy the repository by clicking on the large green "Code" button near the upper right corner of the screen.
3) Right click the .zip file and extract(unzip) it's contents.
4) Open your computer's terminal/console, and navigate to folder called "__Shelter.Solution__". 


**Option 2** (via git console/terminal)
1) Open your Git enabled terminal/console and navigate to a directory that you wish to download this project to.
2) Type the following line of code into your terminal/console to automatically download the project to your current direcory and hit return/enter

    <code>git clone https://github.com/RMGit-it/Shelter.Solution.git</code>

3) Once the project has finished downloading, use the terminal/console to navigate to the "__Shelter.Solution__" folder of the project.


**Setup Database Connection**

Create a new file in the root directory of the _Shelter.Solution/Shelter__ directory named "appsettings.json".  Copy and past the following code inside of the file.

```
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=randel_moore_shelter;uid=YourId;pwd=YourPassword;"
  }
}
```
Replace "YourId" and "YourPassword" in the code above with the user id and user password you use for logging into MySQL Workbench.  Save the "appsettings.json" file.

Type the following code and hit enter/return to install the necessary dependencies. 

<code>dotnet restore</code>

Once the dependencies have installed, type the following commands into your console, hitting enter/return after each.

<code>dotnet ef database update</code>


You can now type the follow code to launch the program...

<code>dotnet run</code>

The API should be available using a web browser at URL: localhost:5000.

## **PostMan**

For full API functionality with JWT, you will want to use a application like Postman to retreive and return an authentication token.  You can download and install the full Postman app for your respective OS using the following link.

[Postman](https://www.postman.com/downloads/)

## **API Documentation**

As this API project is deaulted to using authentication, a web browser will only allow you to access HttpGet routes.  To access all routes and full CRUD functionality, please use Postman for site navigation.

__JSON Web Token (JWT)__
To access full CRUD functionality beyond HttpGet requests, you will need to be authenticated by ther API and receive an JWT.

1. Launch Postman and start a new API request.
2. Enter <code>http://localhost:5000/api/users/authenticate</code> in the "Enter Request URL" field.
3. Set request type to "POST".
4. Click on the "Body" tab under the URL field and enter the following into the text box.

```
    {
        "Username": "Flash",
        "Password": "Thunder"
    }
```
5. Press "Send", and you should recieve a reply from the server that looks similar to the following...

```
{
    "id": 1,
    "username": "test",
    "password": null,
    "role": null,
    "token": "eyJhbGciOiJIUzI2NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE2MTE1MTMwNDMsImv4cCI6MTYxMTU5OTQ0MywiaWF0IjoxMjExNTEzMDQzfQ.8xlwNGS_B1by6CxLebbpFtzZBRFU8aD7IC-T9_t_9Qk"
}
```
6. Click on "Authorization" in Postman (to the left of "Body") and use the dropdown menu to change the Type to "Bearer Token".

7. Copy the token from your authorization request (without quotes), into the Token input box.

This authentication token should be valid for 24 hours.  You now have access to all CRUD functionality.

__Swagger__

While only HttpGet requests will be sucessful, you can explore Swagger Documentation by running the API by typing <code> dotnet run</code> in your terminal, and then navigating to http://localhost:5000/swagger using your web browser.

__CORS__

CORS (Cross Origin Resource Sharing) has been enabled on this project without restrictions.  If this API were deployed, it would allow users outside the API's domain to send requests.  While this is helpful for allowing it's use by the genearl public, it does reduce the security level of the API.

[Microsoft CORS Documentation:](https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0)

--Cross Origin Resource Sharing (CORS):

* Is a W3C standard that allows a server to relax the same-origin policy.
* Is __not__ a security feature, __CORS relaxes security__. An API is not safer by allowing CORS. For more information, see How CORS works.
* Allows a server to explicitly allow some cross-origin requests while rejecting others.
* Is safer and more flexible than earlier techniques, such as JSONP.

<hr>

## __Endpoints__

Base URL: http://localhost:5000/

HTTP Request Structure:

```
GET..../api/{component}
POST.../api/{component}
GET..../api/{component}/{id}
PUT..../api/{component}/{id}
DELETE./api/{component}/{id}

```
{component} can be either "cats" or "dogs".

Example 1: GET /api/cats/

Example 2: GET /api/dogs/5

__Path Parameters__

|Parameter|Type|Default|Required|Description|
|:--------|:------|:------|:-------|:---------------------------|
| id      |int    | none  | false  | Return match by unique ID. |
| name    |string | none  | true   | Return matches by name.    |
| breed   |string | none  | true   | Return matches by breed.   |
| age     |int    | none  | true   | Return matches by age.     |
| sex     |string | none  | true   | Return matches by sex.     |

__Example Query__

http://localhost:/api/cat?breed=sphinx&&sex=male

__Example JSON Response__

```
[  
  {
    "id": 3,
    "name": "Hans",
    "breed": "Sphinx",
    "age": 8,
    "sex": "Male"
  },
  {
    "id": 5,
    "name": "Heathrow",
    "breed": "Sphinx",
    "age": 8,
    "sex": "Male"
  }
]  
```  

__Example POST/PUT__

POST  Http://localhost:5000/api/dogs/

Body: As IDs are generated by the database, do not include them in POST or PUT request bodies.  See example HTTP body below.
```
  {
    "name": "Unfried",
    "breed": "Bombay",
    "age": 7,
    "sex": "Male"
  }
```
## **Planned Features**
No new features are planned at this time.

## **Known Bugs**
There are no known bugs

## **Technology Used**
* C# 7.3
* .NET Core 2.2
* Entity
* Identity
* Git
* MySQL
* dotnet script, REPL
* Swagger (Swashbuckle)
* JSON Web Token Authentication
* CORS
  
## **Authors and Contributors**
Authored by: Randel Moore

## **Contact**
Randel Moore
RMGit.it@gmail.com

## **License**

GPLv3

Copyright © 2020 Randel Moore

