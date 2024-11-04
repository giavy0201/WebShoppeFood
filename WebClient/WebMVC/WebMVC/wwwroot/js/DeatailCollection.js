$(document).ready(function () {
    var collectionIdDefault = $('.list-store-collection').attr('CollectionID');;
    var numberDefault = 12;
    var pageIndexDefault = 1;
    updateListStore(collectionIdDefault, numberDefault, pageIndexDefault)

    function updateListStore(CollectionID, NumberOfItem, IndexPage) {
        $.ajax({
            url: '/Collection/ListStoreInCollection',
            type: 'POST',
            data: {
                CollectionID: CollectionID,
                NumberOfItem: NumberOfItem,
                PageIndex: IndexPage
            },
            success: function (data) {
                $('.list-store-collection').html(data);
                updatePagination(IndexPage);
            }
        });
    }

    //add paging
    function updatePagination(pageIndex) {
        $('.pagination').empty();

        $.ajax({
            url: '/Service/StoreByCollection',
            type: 'Post',
            data: {
                CollectionID: collectionIdDefault,
            },
            success: function (data) {
                if (data == null || (data.length / numberDefault) < 1) {
                    $('.pagination').hide();
                }
                else {
                    let count = Math.ceil(data.length / numberDefault);
                    if (count > 1) {
                        for (let i = 1; i <= count; i++) {
                            let newPage = $('<li type="button"></li>');
                            newPage.text(i);

                            if (i === pageIndex) {
                                newPage.addClass('active');
                            }
                            newPage.on("click", function () {
                                document.documentElement.scrollTop = 0;
                                updateListStore(collectionIdDefault, numberDefault, i);
                                $('.pagination li.active').removeClass('active');
                                newPage.addClass('active');
                            });
                            $('.pagination').show();
                            $('.pagination').append(newPage);
                        }
                    }
                }
            }
        });
    }
});
