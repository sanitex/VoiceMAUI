Publishing and installing the application notes:

- To create a self-signed certificate, run the following command in Powershell :
  New-SelfSignedCertificate -Type Custom -Subject "CN=<CN>" -KeyUsage DigitalSignature -FriendlyName "VoiceMAUI cert" -CertStoreLocation "Cert:\CurrentUser\My" -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3", "2.5.29.19={text}")
  - CN has to match the value set in `SanitexVoiceMAUI/Platforms/Windows/Package.appxmanifest` Identity tag Publisher value

- After the certificate is created, copy the thumbprint for it into the .csproj file to replace the current one:
  `<PackageCertificateThumbprint>2AFDD54D7ED3E560D4669C76AC276FC8B0DD0E58</PackageCertificateThumbprint>`

- Choose the target framework as Windows
- Publish for sideloading.
- After publishing is done, go to  
  '...\VoiceMAUI\SanitexVoiceMAUI\bin\Release\net8.0-windows10.0.19041.0\win10-x64\AppPackages\SanitexVoiceMAUI_0.0.4.0_Test'
  You should see the .cert file which needs to be installed to be able to install the application.

Installing the self-signed certificate:

1. Open the Certificate, MyApp.cer, by double-clicking.
2. Click on Install Certificate
3. In the Certificate Import Wizard window,
   3.1. Select Local Machine and click Next
   3.2. Select Place all certificates in the following store
   3.3. Browse and select Trusted Root Certification Authorities
   3.4. Click Next and take defaults. After this you should see a pop-up stating that "The import was successful."
4. You now can proceed to side-load install the .NET MAUI app.
