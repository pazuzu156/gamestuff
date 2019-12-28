#include "game.h"

int main() {
    if (!glfwInit()) {
        log_error("Could not initialize GLFW");
        exit(EXIT_FAILURE);
    }

    GLFWwindow* window;
    window = glfwCreateWindow(WINDOW_WIDTH, WINDOW_HEIGHT, WINDOW_TITLE, NULL, NULL);

    if (!window) {
        log_error("Failed to create window");
        exit(EXIT_FAILURE);
    }

    glfwMakeContextCurrent(window);
    glfwSetKeyCallback(window, key_callback);
    center_window(window);

    if (glewInit() != GLEW_OK) {
        log_error("Could not initialize OpenGL");
        exit(EXIT_FAILURE);
    }

    while (!glfwWindowShouldClose(window)) {
        glClear(GL_COLOR_BUFFER_BIT);
        glfwSwapBuffers(window);
        glfwPollEvents();
    }

    glfwDestroyWindow(window);
    glfwTerminate();

    return 0;
}
