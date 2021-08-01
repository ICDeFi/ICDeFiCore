var AccessControlController = function () {
    this.initialize = function () {
        registerEvents();
    }

    function registerEvents() {
        $(document).on('click', '.CheckedAllById', function () {
            var id = $(this).val();
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                $(".disables_" + id).prop("checked", true);
            } else {
                $(".disables_" + id).prop("checked", false);
            }
        })
    };
}