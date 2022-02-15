module.exports = {
  parser: "@typescript-eslint/parser",
  env: {
    browser: true,
    node: true,
    es6: true,
    jest: true
  },
  extends: ["eslint:recommended", "plugin:react/recommended"],
  settings: {
    react: { version: "detect" }
  },
  parserOptions: {
    ecmaFeatures: {
      jsx: true
    },
    ecmaVersion: 2018,
    sourceType: "module"
  },
  plugins: ["react", "react-hooks"],
  rules: {
    "react-hooks/rules-of-hooks": "error",
    "react-hooks/exhaustive-deps": "warn"
  }
};
