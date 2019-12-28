# games

Stuff to build OpenGL exes Discord will recognize to show off playing titles outside of PC or XBox.

## Building

To build this, you need a few things:

* [Msys2](https://www.msys2.org/)
* [DotNet Core](https://dotnet.microsoft.com/download)
* Glew and GLFW

Glew and GLFW can be obtained with Msys by running `pacman -S mingw-w64-x86_64-{glew,glfw}`

When building, run `./build -b` within MINGW64 environment for Msys. Make sure dotnet is in your msys's PATH variable. Simply adding `export PATH=$PATH:/c/Program\ Files/dotnet` to your `.bashrc` file will suffice.

## Cleaning

Running `./build -c` will clean up any generated files for you.

## Words of Note

Since this is specialized for Windows, this only works for Windows. If you want to adapt it for other systems, go ahead. It's not too difficult to remove Msys requirements from this for other systems.
