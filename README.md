# FoodMenu

#Prerequesties:
IDE with .net 7 SDK

#How to run program(Tested with Visual Studio 2022):

! IMPORTANT ! Before any steps below make sure to update Visual Studio IDE that supports .NET SDK 7

1. Run the program F5 (without debugger) or CTRL+F5(with debugger)
2. Swagger UI page will open a new tab.
3. Select arrow for dropdown menu for ENDPOINT with GET method called "/api/v1/meals/{name}".
4. Click "Try it out".
5. Inside text box (near name) write a name of a meal.
6. Click "Execute".
7. Following details will be shown under Responses tab: (Curl request, Request URL, Server response with status code and details(response body will contain information about the made request response).

#Testing(Tested with Visual Studio 2022):
1. Select "Test" tab in menu bar.
2. From drop-down menu select "Test Explorer".
3. Window of "Test Explorer" should open up.
4. Click first green triangle at the top right.
5. Tests will run and output will be shown in "Test Explorer" tab.
