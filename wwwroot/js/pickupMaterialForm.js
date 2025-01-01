let materials = window.materials;
let inventories = window.inventories;
let rowCount = window.rowCount || 0;

let pickedInventories = [];

$(document).ready(function () {
  let insertMaterialBtn = $("#insert-material-btn");

  //   handle barcode input
  insertMaterialBtn.on("click", function (e) {
    let materialId = Number($("#material-select").val());

    if (!materialId)
      return swal({
        title: "No Material Selected",
        text: "Please select a material",
        icon: "error",
      });

    let inventory = findInventory(materialId);

    if (!inventory || inventory.Quantity === 0) {
      return swal({
        title: "Not enough material",
        text: "Request this material to get more",
        icon: "error",
      });
    }

    handlePickedInventory(inventory);

    $("#material-select").val("");

    updateTable();
  });

  //   handle pickup quantity input
  $(document).on("change paste keyup", ".pickup-quantity-input", function () {
    var value = $(this).val();

    console.log();

    var inventoryId = $(this).data("id");

    var inventory = pickedInventories.find((i) => i.Id == inventoryId);

    inventory.pickupQuantity = Number(value);
  });

  //   handle delete row button
  $(document).on("click", ".delete-row-btn", function () {
    var inventoryId = $(this).data("id");

    pickedInventories = pickedInventories.filter((i) => i.Id != inventoryId);

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

  $("#submit-btn").on("click", function () {
    if (pickedInventories.length === 0)
      return swal({
        title: "No Material Selected",
        text: "Please select a material",
        icon: "error",
      });

    $("#pickup-form").submit();
  });
});

function findInventory(materialId) {
  let inventory = inventories.filter((i) => i.MaterialId === materialId)[0];
  console.log(inventory);
  return inventory;
}

function handlePickedInventory(inventory) {
  let alreadyPickedInventory = pickedInventories.find(
    (i) => i.Id == inventory.Id
  );

  if (alreadyPickedInventory) {
    alreadyPickedInventory.pickupQuantity += 1;
  } else {
    pickedInventories.push({
      ...inventory,
      pickupQuantity: 1,
    });
  }
}

function updateTable() {
  let rowHtml;
  rowCount = 0;

  pickedInventories.forEach((i) => {
    console.log(i);
    rowHtml += `
            <tr class="material-row">
              <td>
                <span>${i.Material.Name}</span>
                <input type="hidden" readonly name="InventoryForms[${rowCount}].InventoryId" value="${
      i.Id
    }" />
              </td>
              <td>${i.Quantity + " " + i.Material.DetailMeasurement} </td>
              <td>
                <div class="form-group">
                <input type="number" name="InventoryForms[${rowCount}].Quantity" class="form-control pickup-quantity-input" value="${
      i.pickupQuantity
    }" data-id="${i.Id}" min="1" max="${i.Quantity}"/>
                
                </div>
              </td>
              <td><button class="btn btn-danger delete-row-btn" data-id="${
                i.Id
              }" >x</button></td>
            </tr>
          `;

    rowCount++;
  });

  $("#table-body").html(rowHtml);
}
