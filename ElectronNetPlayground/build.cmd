dotnet build
electronize build /target linux /PublishReadyToRun false

dotnet publish -r linux-arm --output "obj\desktop\linux-arm\bin"

cd obj\desktop\linux-arm
xcopy ..\linux\api .\api
xcopy ..\linux\main.js .\
xcopy ..\linux\package.json .\
xcopy ..\linux\package-lock.json .\

npm install
npm install electron-packager --global

electron-packager . --platform=linux --arch=armv7l --out="electron-packager . --platform=linux --arch=armv7l --out="..\..\..\bin\desktop-arm" --overwrite" --overwrite

echo "DONE"