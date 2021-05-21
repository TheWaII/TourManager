# TourManager
This is a desktop application where you can **create** tours. These tours consist of *name*, *source*, *destination*, *distance* and *description*. The added tours can also be **edited** and **deleted**. These tours can also have logs, which can either be *unspecified*, *bike log* or a *car log*. The logs consist of *name*, *date*, *distance*, *total time*, *rating*, and *report*. If the user wished to add *bike* or *car log*, they are offered additional parameters which they can add. These include *peak heart rate (HR)*, *lowest HR*, *average HR*, *average speed* and *calories burnt* for bike logs and *max speed*, *average speed*, *fuel used*, *fuel cost* and *caught speeding* for car logs.

## App Architecture

### Layers and Layer contents / functionality

The MVVM pattern was used as the architectural design. There is one **WPF library** and 4 other class libraries. Following layers were implemented: **Bussiness Layer**, **Data Access Layer**, **Model Layer** and a **Moq Layer**.

#### WPF Libaray
View Models and UI design is implemented in this layer as the name WPF suggests. This layer communicates only with the bussiness layer. 

There is only one window which is the main window. In this window there are many smaller controls which have their own seperate control xaml files. 

In total, there are 7 view models. There is a MainViewModel.cs , which calls all the other view models in the constructor. All the bindings in the xaml files are done via mainviewmodel. 

#### Bussiness Layer
This is the layer where all the logic is written into. Lists are filled in this layer and passed on to the WPF layer. This layer is not connected to the database, meaning the data form the database are collected via the Data Access Layer.

#### Data Access Layer

Here all the CURD operations for the database are written. This layer communicates with the Bussiness Layer.

#### Model layer

Here the models are written. For each table in the database there is a model class which has all the column values of the table as parameters. For eg. for the tour table there is a *tourdata.cs* which includes *int tourId*, *string tourName*, *string tourSource*, *string tourDestination*, *double tourDistance*, *string tourDescription* and *string tourRoute*.


#### Moq Layer 

This layer is for testing. There are tests which mock the database (as the name moq suggests) and check if the CURD functionality is called correctly,

## Architectural, UX, library decisions

The MVVM pattern was chosen as it was required for this project. The UX design was also somewhat premade for orientation with some exceptions.

![Class diagram](img/Readme/classDiagram.png)


My goal with the design was to make a seemless design where the user does not have to open many different windows but can work on a single window without losing the track of where to find something. This is the reason I do not have any extra windows (except for error messages and help). 

## Implemented design pattern 

In this project all the data are to be saved in a Postgres Database. To communicate with the database I implemented a repository pattern. This decision was made because I wanted a clean code. This also allowed me to mock the database for unit testing.

## Unit testing decisions
I tested the databse functionality because I wanted to check if everything works as expected without messing around with the database sever itself. Mocking allowed me to test all the functionalities. In total I have 20 Unit tests.

## Unique features
A unique feature I have built in is the dark mode. This allows the user to switch the color scheme of the application. If they want a more brighter look, they can set the color scheme to default. However, for people who are more comfortable with a dark enviorment, they may turn on the dark mode by clicking the lamp button on the top right corner. 

![Dark mode](img/Readme/darkmode.gif) 


##### Total time:  75.4
