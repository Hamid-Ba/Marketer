function checkStockWhenAddingHappen(id, count) {
    $.ajax({
        url: "https://localhost:5001/Order/CheckStock",
        method: "POST",
        timeout: 0,
        headers: {
            "Content-Type": "application/json"
        }
            data: JSON.stringify({ "productId": id, "count": count }),
    }).done(function (data) {
        if (data.isInStock == false) {
            const warningsDiv = $('#productStockWarnings');
            if ($(`#${id}`).length == 0) {
                warningsDiv.append(`
                    <p class="alert alert-warning" id="${id}">
                        <i class="fa fa-warning"></i> کالای
                        <strong>${data.productName}</strong>
                        در انبار کمتر از تعداد درخواستی موجود است.
                    </p>
                `);
            }
        } else {
            if ($(`#${id}`).length > 0) {
                $(`#${id}`).remove();
            }
        }
    });
}