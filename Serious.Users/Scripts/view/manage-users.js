$(document).ready(function () {
    window.adminUser = {
        logOffUser: function (username)
        {
            var url = $("#LogOutUrl").val();
            var data = new FormData();
            data.append("email", username);
            $.ajax({
                type: "POST",
                url: url,
                contentType: false,
                processData: false,
                data: data,
                success: function (result) {
                    
                }
            });
        }
    }
});