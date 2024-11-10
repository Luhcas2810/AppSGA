document.getElementById("logoutButton").addEventListener("click", async function (event) {
    event.preventDefault();
    const Codigo = CodigoUsuario;
    try {
        const response = await fetch(postLogoutURL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ Codigo })
        });

        const result = await response.json();

        if (result.data.Respuesta) {
            window.location.href = result.data.Redireccion;
            swal("Bienvenido", result.data.Mensaje, "success");
        } else {
            swal("Error", "No es posible cerrar sesión", "warning");
        }
    } catch (error) {
        console.error("Error en la solicitud:", error);
    }
});