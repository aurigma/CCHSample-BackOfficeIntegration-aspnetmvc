// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// A helper function to download the URL
// https://stackoverflow.com/a/49917066/4173445
function download(url) {
	let link = document.createElement("a");
	link.style = "position:absolute; top:-99999999; left:-9999999; visibility:hidden";
	link.href = url;
	document.body.appendChild(link);
	link.click();
	document.body.removeChild(link);
};
