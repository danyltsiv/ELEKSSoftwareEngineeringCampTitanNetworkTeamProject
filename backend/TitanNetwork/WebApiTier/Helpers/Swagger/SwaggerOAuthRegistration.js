(function () {
    $(function () {
        var OAuthUI =
            '<div class="input"><input placeholder="username" id="input_username" name="username" type="text" size="10"></div>' +
            '<div class="input"><input placeholder="password" id="input_password" name="password" type="password" size="10"></div>';
        $(OAuthUI).insertBefore('#api_selector div.input:last-child');
        $("#input_apiKey").hide();

        $('#input_username').change(addAuthorization);
        $('#input_password').change(addAuthorization);
    });

    function addAuthorization() {
        var username = $('#input_username').val();
        var password = $('#input_password').val();
        if (username && username.trim() != "" && password && password.trim() != "") {
            $.ajax({
                url: "http://titanapidev.azurewebsites.net/token",
                type: "post",
                contenttype: 'x-www-form-urlencoded',
                data: "grant_type=password&username=" + username + "&password=" + password,
                success: function (response) {

                    var bearerToken = "Bearer " + response.access_token;

                    window.swaggerUi.api.clientAuthorizations.remove('api_key');

                    var apiKeyAuth = new SwaggerClient.ApiKeyAuthorization("Authorization", bearerToken, "header");

                    window.swaggerUi.api.clientAuthorizations.add('oauth2', apiKeyAuth);

                    alert("Login Succesfull!");

                },
                error: function (xhr, ajaxoptions, thrownerror) {
                    alert("Login failed!");
                }
            });
        }
    }
})();