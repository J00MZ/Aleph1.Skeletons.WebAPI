# Aleph1.Skeletons.WebAPI
This extension will add a new WebAPI Project Template Skeleton type for WebAPI projects.  

### Project Template Features
* N-Tier project using DI (includes DAL-BL-API with moqs).
* WebAPI Auth using Tokens, with custom security project (includes moq).
* WebAPI Throttling on all controllers.
* Enables Swagger automatically (with Documentation).
* Logging using PostSharp and NLOG (configuration set to local file).
* Friendly exception handling on the webapi controllers.
* ModelValidation on the webapi controllers (hebrew locale by default).

# Prerequisites
* [Visual Studio](https://www.visualstudio.com/) 2017
* [VS Extensibility Tools](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ExtensibilityTools) installed.
* [Sidewaffle Creator (2017)](https://marketplace.visualstudio.com/items?itemName=Sayed-Ibrahim-Hashimi.SidewaffleCreator2017) installed.

# Installation
* Clone the project
* Run the Package project to create the .vsix
* Installable .vsix in [Visual Studio](https://www.visualstudio.com/) 2017.

# TODO
* Add export functionality by default (with excel/pdf from nuget)
