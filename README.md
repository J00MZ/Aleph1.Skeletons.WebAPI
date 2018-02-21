# Aleph1.Skeletons.WebAPI

Create a VSIX for VisualStudio 2017.
this will add a new Project Template that gives you the ability to create a web api project from scratch.
this prject will auto include:
* N-Tier project (including: DAL-BL-API with moqs) using DI.
* Enabling Swagger automaticly in the project (with Documentation for your code)
* Logging using PostSharp and NLOG (config already set to local file)
* Friendly exception handling on the webapi controllers

TODO:
* add ticket security by default (with encryption/decryption logic from nuget)
* add export functionality by default (with excel/pdf from nuget)

HOW TO USE:
* install VS 2017
* install [vs Extensibility Tools](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ExtensibilityTools)
* install [SideWaffle Template Pack](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.SideWaffleTemplatePack)
* run the Package project - it will create a VSIX that can be installed in VS2017.
