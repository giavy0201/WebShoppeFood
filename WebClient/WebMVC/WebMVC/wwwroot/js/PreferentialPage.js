$(document).ready(function () {
    let InfoHeader = JSON.parse(localStorage.getItem('Info_Header'))
    var CityIdPre = InfoHeader.CityIdHeader;
    var CateIdPre = InfoHeader.CateIdHeader;
    let DistrictIDs;
    var numberDefault = 12;
    var pageIndexDefault = 1;

    ListDistrict(CityIdPre);
    updateListStorePrePage(CityIdPre, CateIdPre, DistrictIDs, numberDefault, pageIndexDefault)

    //load list district
    function ListDistrict(CityIdPre) {
        $.ajax({
            url: '/Product/ListDistrictPre',
            type: 'Get',
            data: {
                CityID: CityIdPre,
            },
            success: function (data) {
                $('.option-list-district').html(data);
            }
        });
    };

    //click option district
    $('#preferential-option-area').click(function (event) {
        event.stopPropagation();
        var mainPre = $(this).closest('.main-content');
        var districtPre = mainPre.find('.preferential-option-left-distrist');
        districtPre.toggle();
    });
    //click option district hide
    $(document).click(function (event) {
        var districtPre = $('.preferential-option-left-distrist');
        if (!districtPre.is(event.target) && districtPre.has(event.target).length === 0) {
            districtPre.hide();
        }
    });

    //click district checkbox
    $(document).on('change', '.district-checkbox', function () {
        DistrictIDs = [];
        $(".district-checkbox:checked").each(function () {
            DistrictIDs.push($(this).val());
        });
        updateListStorePrePage(CityIdPre, CateIdPre, DistrictIDs, numberDefault, pageIndexDefault);
    })

    //load store
    function updateListStorePrePage(CityID, CateID, Districts, NumberOfItem, IndexPage) {
        $.ajax({
            url: '/Product/ListStorePreferentialPage',
            type: 'POST',
            data: {
                CityID: CityID,
                CateID: CateID,
                Districts: Districts,
                NumberOfItem: NumberOfItem,
                PageIndex: IndexPage
            },
            success: function (data) {
                $('.list-store-preferentialPage').html(data);
                updatePagination(NumberOfItem, IndexPage);
            }
        });
    };

    //add paging
    function updatePagination(numberOfItem, pageIndex) {
        $('.pagination').empty();

        $.ajax({
            url: '/Service/ListStorePreferentialPage',
            type: 'Post',
            data: {
                CityID: CityIdPre,
                CateID: CateIdPre,
                Districts: DistrictIDs
            },
            success: function (data) {
                if (data == null || (data.length / numberOfItem) < 1 ) {
                    $('.pagination').hide();
                }
                else {
                    let count = Math.ceil(data.length / numberOfItem);
                    if (count > 1) {
                        for (let i = 1; i <= count; i++) {
                            let newPage = $('<li type="button"></li>');
                            newPage.text(i);

                            if (i === pageIndex) {
                                newPage.addClass('active');
                            }
                            newPage.on("click", function () {
                                document.documentElement.scrollTop = 0;
                                updateListStorePrePage(CityIdPre, CateIdPre, DistrictIDs, numberOfItem, i);
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
