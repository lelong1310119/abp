$(function () {
    var l = abp.localization.getResource('Todo');
    var createModal = new abp.ModalManager(abp.appPath + 'Provinces/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Provinces/EditModal');
    var deletedModal = new abp.ModalManager(abp.appPath + 'Provinces/DeletedModal')

    var dataTable = $('#ProvincesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(todo.provinces.province.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('Todo.Provinces.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('Todo.Provinces.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'ProvinceDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        todo.provinces.province
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Code'),
                    data: "code"
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Description'),
                    data: "description"
                },
                {
                    title: l('AreaCode'),
                    data: "areaCode"
                },
                {
                    title: l('OrderBy'),
                    data: "orderBy"
                },
            ]
        })
    );
    var dataTableDeleted = $('#ProvincesDeletedTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(todo.provinces.province.getListDeleted),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Restore'),
                                    visible: abp.auth.isGranted('Todo.Provinces.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'ProvinceRestoreConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        todo.provinces.province
                                            .restore(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyRestore')
                                                );
                                                dataTableDeleted.ajax.reload();
                                            });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('Todo.Provinces.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'ProvinceDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        todo.provinces.province
                                            .confirmDelete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTableDeleted.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Code'),
                    data: "code"
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Description'),
                    data: "description"
                },
                {
                    title: l('AreaCode'),
                    data: "areaCode"
                },
                {
                    title: l('OrderBy'),
                    data: "orderBy"
                },
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProvinceButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});