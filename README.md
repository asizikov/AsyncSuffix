# AsyncSuffix plugin for [ReSharper](https://www.jetbrains.com/resharper/) 10

##CI Statuses


| Master  | Develop | Feature |
| ------- | ------- | ------- |
| [![Build status](https://ci.appveyor.com/api/projects/status/jo72bgcj0twlskbt/branch/master?svg=true)](https://ci.appveyor.com/project/asizikov/asyncsuffix/branch/master)  | [![Build status](https://ci.appveyor.com/api/projects/status/jo72bgcj0twlskbt/branch/develop?svg=true)](https://ci.appveyor.com/project/asizikov/asyncsuffix/branch/develop)  | [![Build status](https://ci.appveyor.com/api/projects/status/jo72bgcj0twlskbt?svg=true)](https://ci.appveyor.com/project/asizikov/asyncsuffix) |


ReSharper plugin.


Appends 'Async' suffix to Task returning method name.

![In action](http://asizikov.github.io/images/async-suffix-plugin/in-action.gif)

[Description](http://asizikov.github.io/2015/08/02/async-suffix-resharper-plugin/)

## Custom Async types support

Sometimes you have a type which you want to be treated as asynchronous.

Since v1.2.0 plugin supports custom types:

![customasynctype](https://cloud.githubusercontent.com/assets/819053/11931954/0b9996ca-a7f2-11e5-8fce-99a82449737a.PNG) 