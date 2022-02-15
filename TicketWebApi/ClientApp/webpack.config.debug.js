﻿const webpack = require("webpack");
const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

process.env.NODE_ENV = "development";

module.exports = {
	mode: "development",
	target: "web",
	devtool: "source-map",
	entry: { main: ["./src/index.tsx"] },
	output: {
		path: path.resolve(__dirname, "build"),
		publicPath: "/",
		filename: "bundle.[contenthash].js",
	},
	devServer: {
		publicPath: "/",
		openPage: "/",
	},
	plugins: [
		new MiniCssExtractPlugin({
			filename: "[name].[contenthash].css",
		}),
		new webpack.DefinePlugin({
			"process.env.NODE_ENV": JSON.stringify(process.env.NODE_ENV),
			"process.env.API_URL": JSON.stringify("/formerstudents/api"),
		}),
		new HtmlWebpackPlugin({
			template: "src/index.html",
			minify: {
				// see https://github.com/kangax/html-minifier#options-quick-reference
				removeComments: true,
				collapseWhitespace: true,
				removeRedundantAttributes: true,
				useShortDoctype: true,
				removeEmptyAttributes: true,
				removeStyleLinkTypeAttributes: true,
				keepClosingSlash: true,
				minifyJS: true,
				minifyCSS: true,
				minifyURLs: true,
			},
		}),
	],

	resolve: {
		extensions: [".ts", ".tsx", ".js", "jsx", ".json"],
	},

	module: {
		rules: [
			{
				test: /\.tsx?$/,
				use: ["babel-loader"],
			},
			{
				test: /\.(js|jsx)$/,
				exclude: /node_modules/,
				use: ["babel-loader"],
			},
			{
				test: /\.m?js/,
				resolve: {
					fullySpecified: false,
				},
			},
			{
				test: /\.(css|sass|scss)$/,
				use: [
					MiniCssExtractPlugin.loader,
					{
						loader: "css-loader",
						options: {
							importLoaders: 2,
							sourceMap: true,
						},
					},
					{
						loader: "sass-loader",
						options: {
							sourceMap: true,
						},
					},
				],
			},
		],
	},
};
