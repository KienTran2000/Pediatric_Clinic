function MessageBox(Title, Icon) {
    Swal.fire({
        text: Title,
        icon: Icon,
        showConfirmButton: false,
        timer: 5000
    })
}