﻿# Welcome to Customer's Canvas Integration Demo App

This is a sample Customer's Canvas integration application. It demonstrates various aspects of the Customer's Canvas API usage. If you need to integrate Customer's Canvas with your web application, feel free to use this app as a starter kit and a playground. 

## How to get it to work?

### Building and running the app

Just clone this repo, open in Visual Studio and run it. We were using Visual Studio 2019 to create this app.

If you are not a big fan of Visual Studio, you may use any other editor (e.g. Visual Studio Code) and use `dotnet build` / `dotnet run` commands to work with it.

### Managing Customer's Canvas sensitive settings

It is necessary to provide the app with some settings like your Customer's Canvas account (tenant) ID, your app client ID/secret key and some other. You may do it in two ways: 

  * Quick but dirty way - add the keys described below into **appsettings.json**
  * Use the [ASP.NET Core app secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets) functionality

If you are not familiar to the secrets, it may be tempting to skip it and just use **appsettings.json**, but it is highly recommended spending a few minutes to learn how to deal with it. This way, you will avoid leaking sensitive data such as secret keys of your app to repos, etc. 

In short, these are several methods how you deal with it: 

  * Through Visual Studio - right click a solution and choose **Manage User Secrets**. It will open a **secrets.json** which works pretty much the same as **appsettings.json**.
  * Through command line - first run `dotnet user-secrets init`, then add your secrets using `dotnet user-secrets set "CustomersCanvas:KEYNAME" "VALUE"`
  * By editing the **secrets.json** in `%APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json`. See the article linked above for details.
  
### Settings

The sample app expects that the following app settings are available: 

```
  "CustomersCanvas:TenantId": "<SEE BELOW>",
  "CustomersCanvas:StorefrontId": "<SEE BELOW>",
  "CustomersCanvas:IdentityProviderUrl": "https://customerscanvashub.com",
  "CustomersCanvas:DesignEditorVersion": "<SEE BELOW>",
  "CustomersCanvas:ClientSecret": "<SEE BELOW>",
  "CustomersCanvas:ClientId": "<SEE BELOW>",
  "CustomersCanvas:ApiUrl": "https://api.customerscanvashub.com"
```

#### TenantId

This is an ID of your account in Customer's Canvas. If you don't have it yet, [contact our support team](https://customerscanvas.com/account/cases) for details on this.

#### StorefrontId

You need to register your storefront in your account to be able to integrate it with Customer's Canvas. To do it, go to **Settings** -> **Integrations** and click **Create new**. 

Choose type **Custom**. Fill the **Name** and **Allow at** fields. You may leave other fields blank.

#### IdentityProviderUrl / ApiUrl

Unless you are using an on-premises version of Customer's Canvas, set it to `https://customerscanvashub.com` and `https://api.customerscanvashub.com` correspondingly.

#### DesignEditorVersion

A version of the Design Editor installed at your account. [Contact our support team](https://customerscanvas.com/account/cases) if you don't have it. 

#### ClientSecret / ClientId

In addition to the storefront, you also need to register your application to get the authorization data. 

Go to **Settings** -> **External apps**, then click **Create**. Give a name to your app and choose the permissions you want to provide. Then open it and copy the appropriate values to the settings.

Note, it is recommended to give as little rights as possible. For experimental purposes you may create an app with full permissions, but we would recommend deleting it once you finish exploring the API. 

## How this app is organized? 

The app is a traditional MVC application. The Views folder contains the frontend code ("vanilla JS" with Razor template engine), the Controllers folder is a "bridge" between the frontend and backend, the Services folder contains the code which does some work on the backend. The **Startup.cs** configures Dependency Injection, in particular, for the API Client libraries - see [Using API in a C# application tutorial](https://customerscanvas.com/docfx/dev/tutorials/cs-api-client.html?tabs=dotnetcore) for more information.

This is a single application for several use cases. Right now there are two of them are implemented: 

1. Demo 1 - editing a design from Customer's Canvas directly (no specific ecommerce integration)
2. Demo 2 - integrating Customer's Canvas to an ecommerce system

Let's describe each of them in more detail.

### Edit Designs Directly

This scenario is implemented with the **DesignsController.cs** (and the appropriate views in the **Designs** folder). In addition, there is a **DesignsPreviewController.cs** which helps to request the preview images for the designs to display them in a list.

Let's explore the actions (and appropriate views) for the `DesignsController`.

#### Index action

It gives you an example how you can list all designs from a root folder of your account and show them on a page. It also demonstrates a recommended way how you create preview images.

#### Edit action

It explains how you can open a Design Editor using a classic IFrame API and download a result as an image. 

You will find the information how to configure the editor and work with the IFrame API JS library at [classic Design Editor docs](https://customerscanvas.com/docs/cc/IframeApi-introduction.htm).

#### EditWithUIF action

It explains how you can open a Design Editor using a UI Framework JS library.  

You will find the information how to create configs for UI Framework and how it works at [UI Framework guide](https://customerscanvas.com/support/ui-framework).

> **NOTE:**
> You may wonder what are the benefits of UI Framework vs IFrame API. Although IFrame API is easier to start with, UI Framework gives much more flexibility. It allows isolating all the editor specific code to separate JSON files called _configs_. This way you may store the editor settings separately from your code. 
> So if you offer your customers more than a single product category, it is highly recommended to use UI Framework approach. 
> BTW, the UI Framework is used if you are integrating editors from BackOffice (see the next section).

### Integrate with e-commerce platform

When we are talking about integration of Customer's Canvas with some ecommerce platform, as usual, the following tasks arise: 

   * Associating Customer's Canvas entities with the products in your ecommerce system. 
   * Opening the editor on the product page.
   * Saving the results in Customer's Canvas.

#### Associating designs with products

As usually, you want to do it in the admin panel of your ecommerce, in the same place where you manage your product catalog. 

The **AdminController.cs** contains a very simplified version of such admin panel. It works with a simple database of the "ecommerce system product" (based on SQLite for brevity). There are pretty standard actions like list and edit, and a several of Customers' Canvas specific ones.

Customer's Canvas stores the data you want to connect to the product in an entity called _Product Specification_. The product specifications store a link to the _editor_ (which is basically an UI Framework config) and the _attribute values_. Each editor may require different set of attributes. For example, a _Template-based Print Product_ requires you to provide a design while the _Blank Print Product_ requires a width and height. Go to your account, and create some product specifications.

To create a connection between a product specification and your product, it is necessary to create another entity in Customer's Canvas - _Product Reference_. It binds three things together: the product in your system, the Customer's Canvas product specification, and the _storefront_. 

The `ConnectProduct` action does this connection (and the `DisconnectProduct` removes it). We just show a list of a all product specification for each product and call the `ConnectProduct` for a specified product.
 
#### Opening the editor

Now let's take a look what happens on the storefront. Here, you want to list all your products and when a user opens a product, display an editor as per the Product Specification with the content as per the attributes the store employee have selected.

To simplify things, we show here only the products which are associated with a product specifications. The product page contains a Personalize button which leads to the **Personalize.cshtml** view. Here, we are using a special JS library called **storefront.main.js**. You may find it in **wwwroot/js** folder.

This script hides all the UI Framework complexities. All you need is to specify the product ID you want to load along with some init data. The script will do all the "heavy lifting".

The only place where you need to add your custom logic is the code which executes when the user finishes editing and clicks "Add to cart" or "Finish" button. You can do it by adding the `onFinish` event handler. Here, you are receiving a JSON object representing a _Project_ which we will discuss in the next section.

#### Saving the project

The _Project_ contains the information about user's personalization. Typically, it includes the _state id_ (a Private Design ID), _user id_, the order metadata as _fields_, and some other data.

It is supposed that you should store the project object along with your shopping cart data. Once the customer completes the order and makes a payment, you need to create a Project in Customer's Canvas. 

To simplify things, we have omitted all the shopping cart functionality and submitting the Project directly. It happens in the **ProjectsController.cs**.

## Useful links

For better understanding of what happens in this code, you may want to explore Customer's Canvas docs:

  * [Official Customer's Canvas Developer Guide](https://customerscanvas.com/docfx/dev/intro.html) - storage services and Storefront API
  * [Documentation of classic Customer's Canvas Design Editor](https://customerscanvas.com/docs/cc/) - main editor app used by the end users (both frontend IFrame API library and backend API)
  * [IU Framework docs](https://customerscanvas.com/support/ui-framework) - a frontend technology which allows for extending the editor interface and making it better connected to your ecommerce platform.

If you run into any problems, feel free to [contact our support team](https://customerscanvas.com/account/cases). We will be happy to discuss your scenarios and help you to get started.