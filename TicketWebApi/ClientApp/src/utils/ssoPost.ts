import axios, { AxiosResponse } from "axios";
declare var require: any;
var DomParser = require("dom-parser");

axios.interceptors.response.use(
	(response) => {
		//are we dealing with json and not dealing with a bad request
		if (response.headers["content-type"].includes("json") && response.status === 200) {
			return response;
		}

		const originalRequest = response.config;

		if (response.headers["content-type"].includes("text/html")) {
			postTokenToServer(response);

			//retry original request
			return axios(originalRequest);
		}
	},
	(error) => {
		return Promise.reject(error);
	}
);

const postTokenToServer = (response: AxiosResponse) => {
	var parser = new DomParser();
	var dom = parser.parseFromString(response.data);

	var form = dom.getElementsByName("hiddenform");
	var url = form[0].getAttribute("action");
	var waNode = dom.getElementsByName("wa");
	var wresultNode = dom.getElementsByName("wresult");
	var wctxNode = dom.getElementsByName("wctx");

	//Remove html encoding
	var wresultCleared = wresultNode[0]
		.getAttribute("value")
		.replace(/&lt;/g, "<")
		.replace(/&gt;/g, ">")
		.replace(/&amp;/g, "&")
		.replace(/&quot;/g, '"');

	var wa = encodeURIComponent(waNode[0].getAttribute("value"));
	var wresult = encodeURIComponent(wresultCleared);
	var wctx = encodeURIComponent(wctxNode[0].getAttribute("value"));

	let urlEncodedData = "",
		urlEncodedDataPairs = [];

	urlEncodedDataPairs.push(encodeURIComponent("wa") + "=" + wa);
	urlEncodedDataPairs.push(encodeURIComponent("wresult") + "=" + wresult);
	urlEncodedDataPairs.push(encodeURIComponent("wctx") + "=" + wctx);

	//Combine the pairs into a single string and replace all %-encoded spaces to
	//the '+' character; matches the behavior of browser form submissions.
	urlEncodedData = urlEncodedDataPairs.join("&").replace(/%20/g, "+");

	var request = new XMLHttpRequest();

	//request.onreadystatechange = function () {
	//    if (request.readyState == XMLHttpRequest.DONE) {
	//        console.log(this.responseText);
	//    }
	//};

	request.open("POST", url, false);
	request.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	request.send(urlEncodedData);
};
