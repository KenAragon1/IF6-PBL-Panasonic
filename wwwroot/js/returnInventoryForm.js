let materials = window.materials;
let inventories = window.inventories || [];
let rowCount = window.rowCount || 0;

console.log(materials);
console.log(inventories);

let validInventories = [];
let pickedInventories = [];

$(document).ready(function () {
  const lineSelect = $("#line-select");
  const materialSelect = $("#material-select");
  const insertMaterialBtn = $("#insert-material-btn");

  lineSelect.on("change", function () {
    resetForm();

    const lineId = $(this).val();

    if (!lineId) {
      materialSelect.attr("disabled", true);
      return;
    }

    validInventories = inventories.filter((i) => i.ProductionLineId == lineId);

    materialSelect.attr("disabled", false);

    updateMaterialSelect();

    console.log(lineId);
  });

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

  function updateMaterialSelect() {
    let html = "<option value=''>select a material</option>";
    console.log(validInventories);

    materials.forEach((i) => {
      let availableQuantity =
        validInventories.find((vi) => vi.MaterialId == i.Id)?.Quantity || 0;
      html += `<option value="${i.Id}">${i.Barcode} - ${i.Name} - ${availableQuantity} available  </option> `;
    });

    materialSelect.html(html);
  }

  function findInventory(materialId) {
    let inventory = validInventories.filter(
      (i) => i.MaterialId === materialId
    )[0];
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
    let rowHtml = " ";
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

  function resetForm() {
    pickedInventories = [];
    updateTable();
  }
});
