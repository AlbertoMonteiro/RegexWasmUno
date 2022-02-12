# RegexWasmUno

This is a web app that I made to help on that issue [Add C# flavor at Regex101](https://github.com/firasdib/Regex101/issues/156).

This app is using Uno WebAssembly and is static hosted in github pages, you can check in this link: https://albertomonteiro.github.io/RegexWasmUno/

# Setup

You will need the stable dotnet 6 sdk installed in your machine.
<br>
Go to downloads page (https://dotnet.microsoft.com/en-us/download), download and install it.
<br>
After install, run 
```bash
dotnet --version
```
You should get something like that
```bash
6.0.101
```

# Running

First clone it
<br>
To run it, just execute the following command
```bash
dotnet publish -c Release -o out
```
That command will produce in the `out` directory the file to run the app.
<br>
Now you can use any tool that servers a directory as a web server, I am using [http-server](https://www.npmjs.com/package/http-server).
<br>
```bash
cd out
http-server -g -b
```
Now open http://localhost:8080 and play with it.

If you look at the `out` directory, you should see something like that: 
```
📂 OUT
│   index.html
│   RegexWasmUno.exe
│   RegexWasmUno.runtimeconfig.json
│   service-worker.js
│
└───package_1796dce6af67d67c2d95b83a253de22aa6ae864f
    │   binding_support.js
    │   binding_support.js.br
    │   corebindings.c
    │   dotnet.js
    │   dotnet.js.br
    │   dotnet.timezones.blat
    │   dotnet.wasm
    │   dotnet.wasm.br
            more files....
```

That app is hosted in GitHub Pages, you can check the gh-pages branch and go to the app site https://albertomonteiro.github.io/RegexWasmUno/.
