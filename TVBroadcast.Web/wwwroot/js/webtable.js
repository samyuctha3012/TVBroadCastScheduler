function highlightCurrentSlotRow() {
    const now = new Date();
    const minutes = now.getMinutes();
    const slotStartMinutes = minutes < 30 ? 0 : 30;

    const slotStartTime = new Date();
    slotStartTime.setMinutes(slotStartMinutes, 0, 0);

    const slotKey = slotStartTime.toTimeString().slice(0, 5); // 'HH:mm'

    // Clear old highlight
    document.querySelectorAll("tr.current-slot").forEach(row => {
        row.classList.remove("current-slot");
    });

    // Highlight current slot
    const row = document.querySelector(`tr[data-time='${slotKey}']`);
    if (row) {
        row.classList.add("current-slot");
    }
}

// Run once on load
highlightCurrentSlotRow();

// Update every minute
setInterval(highlightCurrentSlotRow, 60 * 1000);


//editform js
$(document).ready(function () {
    $(".edit-link").click(function (e) {
        e.preventDefault();

        const row = $(this).closest("tr");
        const id = $(this).data("id");
        const title = row.find("td:eq(1)").text().trim();
        const genre = row.find("td:eq(2)").text().trim();
        const description = row.find("td:eq(3)").text().trim();
        const approval = row.find("td:eq(4)").text().trim();
        const time = row.find("td:eq(0)").text().trim();

        $("#editId").val(id);
        $("#editTitle").val(title);
        $("#editGenre").val(genre);
        $("#editDescription").val(description);
        $("#editApproval").val(approval);
        $("#editTime").val(time);

        $("#editModal").show();
    });

    $(".close").click(function () {
        $("#editModal").hide();
    });

    $(window).click(function (e) {
        if ($(e.target).is("#editModal")) {
            $("#editModal").hide();
        }
    });

    // Form submission via Ajax (Optional: or use regular post)
    $("#editForm").submit(function (e) {
        e.preventDefault();

        $.ajax({
            url: '/Schedule/Edit',
            method: 'POST',
            data: $(this).serialize(),
            success: function () {
                location.reload(); // Refresh page on success
            },
            error: function () {
                alert("Failed to save changes!");
            }
        });
    });
});
//----------delete js--------------
$(document).ready(function () {

    // When delete link is clicked
    $(".delete-link").click(function (e) {
        e.preventDefault();

        const row = $(this).closest("tr");
        const id = $(this).data("id");
        const time = row.find("td:eq(0)").text().trim();
        const title = row.find("td:eq(1)").text().trim();
        const genre = row.find("td:eq(2)").text().trim();
        const description = row.find("td:eq(3)").text().trim();
        const approval = row.find("td:eq(4)").text().trim();

        // Fill modal data
        $("#deleteId").val(id);
        $("#deleteMessage").text(`Are you sure you want to delete "${title}"?`);
        $("#deleteTime").text(time);
        $("#deleteGenre").text(genre);
        $("#deleteDescription").text(description);
        $("#deleteApproval").text(approval);

        $("#deleteModal").show();
    });

    // Close modal on 'x' click
    $(".close-delete").click(function () {
        $("#deleteModal").hide();
    });

    // Cancel button click
    $(".cancel-btn").click(function () {
        $("#deleteModal").hide();
    });

    // Submit delete form
    $("#deleteForm").submit(function (e) {
        e.preventDefault();
        const showId = $("#deleteId").val();

        $.ajax({
            url: `/Schedule/Delete/${showId}`,
            method: 'POST',
            success: function () {
                location.reload(); // refresh the page after deletion
            },
            error: function () {
                alert("Failed to delete the show. Please try again.");
            }
        });
    });
});
