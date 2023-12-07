dotnet clean

dotnet restore management-service.csproj

dotnet publish management-service.csproj -c Release-Linux -r linux-x64 --output /bin/Release

docker build -f .\DockerfileMS -t management .

REM docker run -d -p 7168:7168 --name management management

pause