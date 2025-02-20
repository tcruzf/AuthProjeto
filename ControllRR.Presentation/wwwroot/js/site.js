// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
  var overlay = document.getElementById("overlay");
  var content = document.getElementById("content");

  // Aguarda tudo ser carregado (incluindo imagens)
  window.onload = function () {
      overlay.style.opacity = 0;
      setTimeout(function () {
          overlay.style.display = "none";
          content.style.display = "block"; // Agora mostra o conteúdo
      }, 500);
  };
});