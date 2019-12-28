#include <gl/glew.h>
#include <glfw/glfw3.h>
#include <stdio.h>
#include <stdlib.h>

#define WINDOW_WIDTH 640
#define WINDOW_HEIGHT 480

#ifndef WINDOW_TITLE
#define WINDOW_TITLE "SomeWindowTitle"
#endif

/**
 * Logs an error to the console.
 *
 * @param message The message to log out.
 */
void log_error(const char* message) {
    fprintf(stderr, "[ERROR] %d: %s", errno, message);
}

/**
 * Handles GLFW key input.
 *
 * @param window GLFW window
 * @param key Key
 * @param scancode
 * @param action Key action
 * @param mode
 */
void key_callback(GLFWwindow* window, int key, int scancode, int action, int mode) {
    if (key == GLFW_KEY_ESCAPE && action == GLFW_PRESS) {
        glfwSetWindowShouldClose(window, GL_TRUE);
    }
}

/**
 * Centers app window to monitor.
 *
 * @param window GLFW window
 */
void center_window(GLFWwindow* window) {
    int x, y;
    const GLFWvidmode* vm = glfwGetVideoMode(glfwGetPrimaryMonitor());
    x = (vm->width / 2) - (WINDOW_WIDTH / 2);
    y = (vm->height / 2) - (WINDOW_HEIGHT / 2);
    glfwSetWindowPos(window, x, y);
}
