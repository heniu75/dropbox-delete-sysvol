README

A utility to delete the "System Volume Information" folder within the DropBox sync root.

NOTE YOU MUST CHANGE THE "DropBoxSystemVolPath" key in the application configuration to point to the absolute path of this folder.

E.g. 

  <appSettings>
    <add key="DropBoxSystemVolPath" value="C:\\Dropbox\\System Volume Information"/>
  </appSettings>
