import React, { useEffect, useState } from "react";
import { useRouteError, useNavigate, useLocation } from "react-router-dom";
import { motion, AnimatePresence } from "framer-motion";

const ErrorHandler = () => {
  const error = useRouteError();
  const navigate = useNavigate();
  const location = useLocation();
  const [showDetails, setShowDetails] = useState(false);

  useEffect(() => {
    logError(error);
  }, [error]);

  const logError = (error) => {
    const errorInfo = {
      message: error.message || "An unknown error occurred",
      stack: error.stack,
      type: error.name,
      componentStack: error.componentStack,
      path: location.pathname,
      timestamp: new Date().toISOString(),
      status: error.status,
    };

    console.error("Detailed error information:", errorInfo);
    // sendErrorToLogServer(errorInfo);
  };

  const handleRetry = () => {
    navigate(location.pathname);
  };

  const handleGoHome = () => {
    navigate("/");
  };

  const handleLogin = () => {
    navigate("/login");
  };

  const renderErrorContent = () => {
    const status = error.status || "unknown";
    const errorMessages = {
      "400": {
        title: "400",
        subtitle: "Bad Request",
        message: "Oops! The server cannot understand your request.",
        icon: "❓",
      },
      "401": {
        title: "401",
        subtitle: "Unauthorized",
        message: "Hold on! You need to log in to access this page.",
        icon: "🔒",
      },
      "403": {
        title: "403",
        subtitle: "Forbidden",
        message: "Sorry! You don't have permission to access this page.",
        icon: "🚫",
      },
      "404": {
        title: "404",
        subtitle: "Page Not Found",
        message: "Uh-oh! The page you requested seems to be missing.",
        icon: "🔍",
      },
      "500": {
        title: "500",
        subtitle: "Server Error",
        message:
          "Our bad! There was a problem with the server. Please try again later.",
        icon: "🛠️",
      },
      unknown: {
        title: "Oops!",
        subtitle: "An Error Occurred",
        message: "Something unexpected happened. We're looking into it.",
        icon: "⚠️",
      },
    };

    return {
      ...errorMessages[status],
      color: getErrorColor(status),
      action: status === "401" ? handleLogin : undefined,
    };
  };

  const getErrorColor = (status) => {
    switch (status) {
      case "400":
        return "text-chart-1";
      case "401":
        return "text-chart-2";
      case "403":
        return "text-chart-3";
      case "404":
        return "text-chart-4";
      case "500":
        return "text-chart-5";
      default:
        return "text-primary";
    }
  };

  const errorContent = renderErrorContent();

  return (
    <div className="min-h-screen flex flex-col bg-background text-foreground">
      <header className="py-6 px-4 sm:px-6 lg:px-8">
        <div className="max-w-7xl mx-auto">
          {/* <h1 className="text-3xl font-bold">SI</h1> */}
        </div>
      </header>

      <main className="flex-grow flex items-center justify-center px-4 sm:px-6 lg:px-8">
        <div className="max-w-3xl w-full text-center">
          <motion.div
            initial={{ opacity: 0, y: -50 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5 }}
          >
            <div className={`text-9xl mb-8 ${errorContent.color}`}>
              {errorContent.icon}
            </div>
            <h1 className={`text-6xl font-bold mb-4 ${errorContent.color}`}>
              {errorContent.title}
            </h1>
            <h2 className="text-4xl font-semibold mb-6">
              {errorContent.subtitle}
            </h2>
            <p className="text-xl mb-12 text-muted-foreground">
              {errorContent.message}
            </p>

            <div className="flex justify-center space-x-4 mb-12">
              {errorContent.action ? (
                <motion.button
                  whileHover={{ scale: 1.05 }}
                  whileTap={{ scale: 0.95 }}
                  className="px-8 py-3 bg-primary text-primary-foreground rounded-full hover:bg-primary/90 transition-colors shadow-lg text-lg font-semibold"
                  onClick={errorContent.action}
                >
                  Log In
                </motion.button>
              ) : error.status !== 401 && error.status !== 403 ? (
                <motion.button
                  whileHover={{ scale: 1.05 }}
                  whileTap={{ scale: 0.95 }}
                  className="px-8 py-3 bg-primary text-primary-foreground rounded-full hover:bg-primary/90 transition-colors shadow-lg text-lg font-semibold"
                  onClick={handleRetry}
                >
                  Try Again
                </motion.button>
              ) : null}
              <motion.button
                whileHover={{ scale: 1.05 }}
                whileTap={{ scale: 0.95 }}
                className="px-8 py-3 bg-secondary text-secondary-foreground rounded-full hover:bg-secondary/90 transition-colors shadow-lg text-lg font-semibold"
                onClick={handleGoHome}
              >
                Go Home
              </motion.button>
            </div>

            <motion.button
              whileHover={{ scale: 1.05 }}
              whileTap={{ scale: 0.95 }}
              onClick={() => setShowDetails(!showDetails)}
              className="text-muted-foreground hover:text-foreground transition-colors"
            >
              {showDetails ? "Hide Error Details" : "Show Error Details"}
            </motion.button>

            <AnimatePresence initial={false}>
              {showDetails && (
                <motion.div
                  initial={{ opacity: 0, height: 0 }}
                  animate={{ opacity: 1, height: "auto" }}
                  exit={{ opacity: 0, height: 0 }}
                  transition={{
                    opacity: { duration: 0.2 },
                    height: { duration: 0.3 },
                  }}
                  className="mt-8 overflow-hidden"
                >
                  <motion.div
                    initial={{ opacity: 0, y: 20 }}
                    animate={{ opacity: 1, y: 0 }}
                    exit={{ opacity: 0, y: 20 }}
                    transition={{ duration: 0.3, delay: 0.1 }}
                    className="bg-card text-card-foreground rounded-lg shadow-lg border border-border"
                  >
                    <div className="p-6 text-left">
                      <p className="font-semibold mb-2">Error Details:</p>
                      <p className="break-all mb-4">
                        {error.message || "An unknown error occurred"}
                      </p>
                      {error.stack && (
                        <pre className="text-xs overflow-x-auto bg-muted p-4 rounded text-muted-foreground">
                          {error.stack}
                        </pre>
                      )}
                    </div>
                  </motion.div>
                </motion.div>
              )}
            </AnimatePresence>
          </motion.div>
        </div>
      </main>

      <footer className="py-6 px-4 sm:px-6 lg:px-8">
        <div className="max-w-7xl mx-auto text-center text-sm text-muted-foreground">
          © {new Date().getFullYear()} Danvic Wang. All rights reserved.
        </div>
      </footer>
    </div>
  );
};

export default ErrorHandler;
