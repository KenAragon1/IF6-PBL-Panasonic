// let rowCount = @Model.Forms.Count ;

$(document).ready(function () {
  // let materialInventories = @Html.Raw(System.Text.Json.JsonSerializer.Serialize(Model.MaterialInventories));

  let materialOptionsHtml = "";

  console.log(materialInventories);

  $("#line-select").on("change", function () {
    var selectedoption = $(this).find("option:selected");

    var materialOptions = materialInventories.filter(
      (material) => material.ProductionLineId == selectedoption.val()
    );

    console.log(materialOptions);

    materialOptionsHtml = " <option value=''>Select Material</option> ";
    materialOptions.forEach(function (option) {
      materialOptionsHtml += ` <option value="${option.Id}" data-available-quantity="${option.Quantity}" data-detail-measurement="${option.Material.DetailMeasurement}">${option.Material.Name}</option> `;
    });

    rowCount = 0;
    $("#form-body").html(generateFormHtml(materialOptionsHtml));
  });

  $(document).on("change,#form-body", ".material-select", function () {
    var availableQuantity = $(this)
      .find(":selected")
      .data("available-quantity");
    var detailMeasurement = $(this)
      .find(":selected")
      .data("detail-measurement");

    $(this)
      .closest("js-form-row")
      .find("available-quantity-row")
      .text(`${availableQuantity} ${detailMeasurement}`);
  });

  $("#new-form-btn").on("click", function () {
    $("#form-body").append(generateFormHtml(materialOptionsHtml));
  });

  $(document).on("click", ".remove-row-btn", function () {
    console.log("hello world");
    $(this).closest(".js-form-row").remove();

    $(".js-form-row").each(function (index) {
      $(this)
        .find("select, input")
        .each(function () {
          const nameAttr = $(this).attr("name");
          if (nameAttr) {
            const newName = nameAttr.replace(/\[\d+\]/, `[${index}]`);
            $(this).attr("name", newName);
          }
        });
    });

    rowCount = $("js-form-row").length - 1;
  });
});

function generateFormHtml(materialOptionsHtml) {
  console.log(materialOptionsHtml);
  let html = `< tr class= "js-form-row" >
                                                    <td>
                                                    <div class="form-group">
                                                    <select name="Forms[${rowCount}].MaterialInventoryId" class="form-control">
                                                    ${materialOptionsHtml}
                                                    </select>
                                                    </div>
                                                    </td>
                                                    <td>0   </td>
                                                    <td>
                                                    <div class="py-2">
                                                    <input type="number" name="Forms[${rowCount}].Quantity" id="" class="form-control col-sm-2">
                                                    </div>
                                                    </td>
                                                    <td>
                                                    <button class="btn btn-danger remove-row-btn" type="button">X</button>
                                                    </td>
                                                    </ >
                                                                                                                                                                                                        `;
  rowCount++;
  return html;
}
