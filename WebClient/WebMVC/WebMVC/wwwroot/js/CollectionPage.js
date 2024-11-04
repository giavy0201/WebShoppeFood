$(document).ready(function () {
    let InfoHeader = JSON.parse(localStorage.getItem('Info_Header'))
    var CityIdPre = InfoHeader.CityIdHeader;
    var CateIdPre = InfoHeader.CateIdHeader;
    var numberDefault = 12;
    var pageIndexDefault = 1;
    updateListCollectionPage(CityIdPre, CateIdPre, numberDefault, pageIndexDefault)

    function updateListCollectionPage(CityID, CateID, NumberOfItem, IndexPage) {
        $.ajax({
            url: '/Collection/ListCollectionInPage',
            type: 'POST',
            data: {
                CityID: CityID,
                CateID: CateID,
                NumberOfItem: NumberOfItem,
                PageIndex: IndexPage
            },
            success: function (data) {
                $('.list-collection-page').html(data);
                updatePagination(IndexPage);
            }
        });
    }

    //add paging
    function updatePagination(pageIndex) {
        $('.pagination').empty();
        $.ajax({
            url: '/Service/ListCollection',
            type: 'Post',
            data: {
                CityID: CityIdPre,
                CateID: CateIdPre
            },
            success: function (data) {
                if (data.Data == null || (data.length / numberDefault) < 1) {
                    $('.pagination').hide();
                }
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
                            updateListCollectionPage(CityIdPre, CateIdPre, numberDefault, i);
                            $('.pagination li.active').removeClass('active');
                            newPage.addClass('active');
                        });
                        $('.pagination').show();
                        $('.pagination').append(newPage);
                    }
                }
            }
        });
    }
});
