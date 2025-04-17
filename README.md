# to run: 
1. modify the jwt key, and initial user in server/appsettings.json
2. cd server
3. dotnet run
4. (in a separate window) cd client
5. npm install
6. npm run dev
7. api endpoint should be at localhost:8080, and frontend at localhost:5173
8. if the address given by npm is different, modify the cors address in server/Program.cs
# not production ready
1. would need to use https
2. the client would have to be built with npm run build, and the resulting files served by dotnet (currently not configured for serving files)