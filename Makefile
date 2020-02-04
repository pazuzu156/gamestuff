all: build

build:
	mkdir -pv games
	cp /mingw64/bin/glew32.dll games/
	cp /mingw64/bin/glfw3.dll games/
	dotnet run

clean:
	rm -rf games
	rm -rf bin
	rm -rf obj

.PHONY: all build clean
