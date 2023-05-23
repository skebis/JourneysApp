# Solita Dev Academy assignment

Welcome to Journey app!

Journey app is a simple full stack web application project where you can view and add bike journeys and stations.
Web application is created using .NET Core as a back end and Angular as a front end. Back end has basic CRUD API methods
but front end only uses create (HTTP POST) and read (HTTP GET). Back end uses SQLite database, which will be created in the
solution root directory on first run. Most of the front end elements are created with Angular materials. BE contains unit tests
that test the API controller logic. Tests use an in-memory-database.

Current features (taken from the assignment https://github.com/solita/dev-academy-2023-exercise):
- (BE) Data importing to a database
   - data is validated using a LINQ statement.
- (FE) List of journeys and pagination.
- (FE) List of stations and pagination.
- (FE) Single station view with recommended information.
- (FE EXTRA) Single journey view with departure and return station names and dates.
- Added docker support initially but didn't implement it due to time restrictions.

To run the application you must complete the following steps (every step has been tested with a new Windows 10 Home virtual machine):

1. Clone this repository to a directory.
2. Download Microsoft Visual Studio (tested with VS Community) and install Node.js (https://nodejs.org/en preferably version 18.xx or later). Remember to restart VS after installing Node.js.
3. Install Angular by running the following command in command prompt or terminal: npm install -g @angular/cli
4. Install Visual Studio with ".NET desktop development" and "ASP.NET and web development" workloads.
5. Download the intial database data and place all the .csv files in the same directory where this project's Visual Studio solution file (.sln) is:
   - https://drive.google.com/file/d/1bkQ4McleZjuAwFo5TBlw92NPgI5hdVbz/view?usp=sharing (reupload without finnish special letters)
   - https://dev.hsl.fi/citybikes/od-trips-2021/2021-05.csv
   - https://dev.hsl.fi/citybikes/od-trips-2021/2021-06.csv
   - https://dev.hsl.fi/citybikes/od-trips-2021/2021-07.csv
   - Please keep the file names unchanged because they are hard coded.
6. Open the solution with Visual Studio and run/debug the app at Debug -> Start Debugging or Start Without Debugging. Make sure you are using the "solita-assignment" profile. ![kuva](https://github.com/skebis/solita-assignment/assets/33286562/89aa9fc4-041f-4de4-87fa-831595aa3330)

   - First run will take some time, most likely 15-25 minutes, because the database is initialized with .csv data. Files contain millions of entries. You can watch the import progress from Output -> Show output from Debug.
   - Afterwards your default browser should launch and navigate to localhost:44477 and you are ready to use the app!
7. You can also run the unit tests in Visual Studio by clicking Test -> Run All Tests
