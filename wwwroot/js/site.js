//163.  Confirm the deletion using jquery
$(function () {
    if ($("a.confirmDeletion").length) {
        $("a.confirmDeletion").click(
            () => {
                if (!confirm("Confirm Deletion")) return false;
            });
    }

    if ($("div.alert.notification").length) {
        setTimeout(() => {
            $("div.alert.notification").fadeOut();
        }, 2000);
    }

});
//164.  Confirm deletion of pages
//165.  Now to get Client Side Library 
//166.  Best way to get client side library is through using Library Manager (LibMan)
//***Notes*** Libman is an operations in VS that are based on the content of the project roots library, it's recommended
//to use if you only want to acquire a couple of files.  Libman will let you specify exactly where the files should 
//be placed inside the project
//167.  Right Click Project > Add > Client-Side Library > use cdnjs Provider > search for ckeditor and add all files
//168.  2 Files should be created, ckeditor folder in wwwroot/lib and libman.json
//169.  Go to _Layout.cshtml to use ckeditor


//320.  Create a function for image preview
function readURL(input) {
    //321.  Ned to check if there is a file
    if (input.files && input.files[0]) {
        let reader = new FileReader();

        reader.onload = function (e) {
            $("img#imupload").attr("src", e.target.result).width(200).height(200);
        };
        reader.readAsDataURL(input.files[0]);
        //322.  Go back to Create view page and edit the section to respond to file input
    }
}



    