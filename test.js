function UpdateWithAjax(form, url, method = "get", data, targetSelector) {
  fetch(url, {
    headers: {
      "x-refresh": true,
    },
    body: data,
  })
    .then((res) => res.text())
    .then((text) => {
      $("#user-table").html(text);
      enableAllButtons(this);
    });
}
