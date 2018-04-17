# Lettering Automation

The lettering automation program is a C# application designed to automate the production of lettering artwork.

Internally, the application retrieves order information by constructing SQL statements to run on the AS400 using the Odbc connector, authenticated through Kerberos. Each order's information is passed to the appropriate lettering template, building the artwork if it does not already exist.

Cut lettering is automated, but sew, rhinestone, and sequin lettering is processed for reporting but not automation.


## Configuration

Configuration for the system is stored in a json file `lettering.json`.


The application attempts to read the configuration file from a local `configs` directory. If one is not found, a hard-coded network location is attempted, currently set to `\\vsc-fs01\Lettering\Corel\WORK FOLDERS\Automation\configs`.


The configuration can be managed from within the editor in application, launched with the `Edit Configs` menu item.


When checking days for orders and not specifying a date, weekends are already accounted for, but company holidays are defined in a text file `holidays.txt` located in the same folder as `lettering.json`.


## Setup Check

When beginning the processing of lettering orders, the configuration file will be loaded if it has not yet been loaded. If the configuration file cannot be found or an error occurs, the processing will abort.

The local copy of the Corel automation library is compared to the copy on the network drive. If the local copy is out of date, the network version is copied to the local machine, updating it.

The set of fonts on the network folder are compared to the ones installed. Any locally missing or out of date ones are displayed in a list to the user, and the network font folder is opened, prompting for the user to update the ones necessary. Automatic installation of the needed fonts is problematic but may be possible with more testing.

If either the automation library or fonts were updated, Corel is closed, prompting the user to save if needed. Corel must be restarted to apply the changes.

If setup checking occurs a second time within the same application session, updating needed files may be skipped by the user, enabling the lettering processing to continue.


## Order Processing

If no dates are specified, orders are retrieved that were entered on the previous day. If the previous day is a weekend or holiday, the orders are collected, and the preceeding day is checked, repeating until a work day is found.

For each order found, the filesystem is checked for the built artwork file.
  * If the order's path cannot be determined, the order is logged and skipped for further processing.
  * If the file is found, the order is logged and skipped.
  * If the order needs to be built, the template is opened and building occurs.
  * If the needed template cannot be found, the order is logged, and the user is alerted.


### Art Building

Lettering artwork is built by triggering a VBA macro saved inside the style's Corel template, and final cleanup is left to the artist. The macro is specific to the lettering style but uses some common functionality stored in the shared Automation library.

A template's macro is embedded in the file and is able to be started from a prompt to allow the lettering to be built outside of the automation process.

Due to a limitation with VBA macros stored within Corel templates, they cannot be called directly. In order to work around this limitation, macros are set to activate on a trigger and read embedded data from a shape on the page.

Once the template is opened, the automation program creates an invisible shape on the page that contains the order data embedded within it as metadata.

The program creates a layer named "Automate", triggering the template's macro. The macro reads the metadata containing the order data and calls the main build function with the data.

After the lettering is built by the macro's steps, the artwork is moved to a new page, and a dialog is displayed with options to continue or abort. The dialog allows the user to finish any cleanup or reject the artwork if there is an issue.

Once the artwork is completed, the user clicks "Next" on the dialog.

* The resulting artwork is saved to the desktop in a matching path on the network drive.
* If needed, the artwork is exported in the specified format.
* All open files in Corel are closed.
* The next torder is processed.

Once all orders are processed, a report is generated containing a record of every order, built or not.

The user moves the resulting artwork files from the desktop to the network drive. This is a manual step to prevent any accidental overwriting of existing artwork.


## Macros / Templates

Each automated template contains an embedded VBA macro that constructs the artwork for the style as completely as possible while leaving room for the artist to perform any ncessary cleanup.

As part of the standard macro setup, each template listens for a layer named "Automate" to be created. The trigger reads order data from an invisible shape and calls the primary build function with the data. This chain of functions is a necessary workaround to Corel's limitation of not being able to directly call a function embedded inside of a Corel file.

The build functions are also able to be called by a dialog. This allows a template's macro to be used without the need to go through the automation process.


### Automation Library

While each template contains its own logic for building a lettering style, a common automation library is used to share functionality between templates.

In order to have shared templates be able to read this library and not be based on the use, a path relative to the Corel installation directory is used. However, this means that a Corel version upgrade necessitates a modification to all templates to change the automation library location.


### Template Setup

There is not a single standard methodology to Corel template setup, but the common method of organization of the templates for the macros is through shape and layer naming which can be seen through the object manager.

Generally, building shapes are named relative to their sizing usage (size, spec, etc.) and layers are used to separate building shapes from information.

A "Layer 1" **MUST** exist for most macros as it serves as a workspace for in-progress artwork during its construction.

Typically, an "Art" layer holds the elements used for building the artwork and an "Info" layer holds the elements not used for building and is locked.
