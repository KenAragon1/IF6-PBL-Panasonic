let materials = window.materials;
let rowCount = window.rowCount || 0;

let pickedMaterial = [];

$(document).ready(function () {
  let insertMaterialBtn = $("#insert-material-btn");

  //   handle barcode input
  insertMaterialBtn.on("click", function (e) {
    let materialId = Number($("#material-select").val());

    let material = findMaterial(materialId);

    console.log(material);

    if (material == null || material.availableQuantity === 0) {
      return swal({
        title: "Not enough material",
        text: "Request this material to get more",
        icon: "error",
      });
    }

    handlePickedMaterial(material);

    $("#material-select").val("");

    updateTable();
  });

  //   handle pickup quantity input
  $(document).on("change paste keyup", ".pickup-quantity-input", function () {
    var value = $(this).val();
    var materialId = $(this).data("id");

    var material = pickedMaterial.find((m) => m.id == materialId);

    material.pickupQuantity = Number(value);
  });

  //   handle delete row button
  $(document).on("click", ".delete-row-btn", function () {
    var materialId = $(this).data("id");

    pickedMaterial = pickedMaterial.filter((m) => m.id != materialId);

    $(this).closest("tr").remove();

    $("tr.material-row").each(function (index) {
      $(this)
        .find("input")
        .each(function () {
          const nameAttr = $(this).attr("name");
          if (nameAttr) {
            const newName = nameAttr.replace(/\[\d+\]/, `[${index}]`);
            $(this).attr("name", newName);
          }
        });
    });

    rowCount--;
  });
});

function findMaterial(materialId) {
  let material = materials.filter((m) => m.Id === materialId)[0];
  return {
    id: material.MaterialInventories[0]?.Id || 0,
    name: material.Name,
    measurement: material.DetailMeasurement,
    availableQuantity: material.MaterialInventories[0]?.Quantity || 0,
    pickupQuantity: 0,
  };
}

function handlePickedMaterial(material) {
  let alreadyPickedMaterial = pickedMaterial.find((m) => m.id == material.id);

  if (alreadyPickedMaterial) {
    alreadyPickedMaterial.pickupQuantity += 1;
  } else {
    pickedMaterial.push({
      ...material,

      pickupQuantity: 1,
    });
  }
}

function updateTable() {
  let rowHtml;
  rowCount = 0;

  pickedMaterial.forEach((m) => {
    rowHtml += `
            <tr class="material-row">
              <td>
                <span>${m.name}</span>
                <input type="hidden" readonly name="Forms[${rowCount}].MaterialInventoryId" value="${
      m.id
    }" />
              </td>
              <td>${m.availableQuantity + " " + m.measurement} </td>
              <td>
                <div class="form-group">
                <input type="number" name="Forms[${rowCount}].Quantity" class="form-control pickup-quantity-input" value="${
      m.pickupQuantity
    }" data-id="${m.id}" min="1" max="${m.availableQuantity}"/>
                
                </div>
              </td>
              <td><button class="btn btn-danger delete-row-btn" data-id="${
                m.id
              }" >x</button></td>
            </tr>
          `;

    rowCount++;
  });

  $("#table-body").html(rowHtml);
}
