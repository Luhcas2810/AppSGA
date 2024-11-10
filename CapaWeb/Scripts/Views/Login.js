document.getElementById("loginButton").addEventListener("click", async function (event) {
    event.preventDefault();

    const _usuario = document.getElementById("_usuario").value;
    const contrasenia = document.getElementById("contrasenia").value;

    try {
        const response = await fetch(postLoginURL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ _usuario, contrasenia })
        });

        const result = await response.json();

        if (result.data.Respuesta) {
            window.location.href = result.data.Redireccion;
            swal("Bienvenido", result.data.Mensaje, "success");
        } else {
            swal("Error", result.data.Mensaje, "warning");
        }
    } catch (error) {
        console.error("Error en la solicitud:", error);
    }
});
