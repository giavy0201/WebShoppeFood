$(document).ready(function () {
    let CityIdPre, CateIdPre, listDistrictIds, listContentIds, keyWordInPut;
    let InfoHeader = JSON.parse(localStorage.getItem('Info_Header'))
    CityIdPre = InfoHeader.CityIdHeader;
    CateIdPre = InfoHeader.CateIdHeader;
    var numberDefault = 12;
    var pageIndexDefault = 1;
    keyWordInPut = $('.preferential-option-keyword').attr("keyword");

    LoadListDistrict(CityIdPre);
    LoadListContent(CateIdPre);
    updateListStoreSearch(CityIdPre, CateIdPre, listDistrictIds, listContentIds, keyWordInPut, numberDefault, pageIndexDefault);

    //load list district
    function LoadListDistrict(CityID) {
        $.ajax({
            url: '/Product/ListDistrictPre',
            type: 'Get',
            data: {
                CityID: CityID,
            },
            success: function (data) {
                $('.option-list-district').html(data);
            }
        });
    };

    //load list content
    function LoadListContent(CateID) {
        $.ajax({
            url: '/Search/ListContentSearch',
            type: 'Get',
            data: {
                CateID: CateID,
            },
            success: function (data) {
                $('.option-list-content').html(data);
            }
        });
    };

    // click option
    $('#preferential-option-area').click(function (event) {
        event.stopPropagation();
        var mainHeader = $(this).closest('.preferential-option-left');
        var district = mainHeader.find('.preferential-option-left-distrist');
        district.toggle();
        mainHeader.find('.preferential-option-left-content').hide();
    });

    $('#preferential-option-content').click(function (event) {
        event.stopPropagation();
        var mainHeader = $(this).closest('.preferential-option-left');
        var content = mainHeader.find('.preferential-option-left-content');
        content.toggle();
        mainHeader.find('.preferential-option-left-distrist').hide();
    });

    $(document).click(function (event) {
        var district = $('.preferential-option-left-distrist');
        var content = $('.preferential-option-left-content');
        if (!district.is(event.target) && district.has(event.target).length === 0) {
            district.hide();
        }
        if (!content.is(event.target) && content.has(event.target).length === 0) {
            content.hide();
        }
    });
    //end click option

    //click district - content
    $(document).on('change', '.district-checkbox', function () {
        listDistrictIds = [];
        $(".district-checkbox:checked").each(function () {
            listDistrictIds.push($(this).val());
        });
        updateListStoreSearch(CityIdPre, CateIdPre, listDistrictIds, listContentIds, keyWordInPut, numberDefault, pageIndexDefault);
    });
    $(document).on('change', '.content-checkbox', function () {
        listContentIds = [];
        $(".content-checkbox:checked").each(function () {
            listContentIds.push($(this).val());
        });
        updateListStoreSearch(CityIdPre, CateIdPre, listDistrictIds, listContentIds, keyWordInPut, numberDefault, pageIndexDefault);
    });
    //end click district - content

    //click remove keyword
    const deleteTagButton = document.querySelector(".btn-delete-tag");
    const keywordSpan = document.querySelector(".preferential-option-keyword");

    if (deleteTagButton && keywordSpan) {
        deleteTagButton.on("click", function () {
            keywordSpan.remove();
            deleteTagButton.remove();
            location.reload();
            updateListStoreSearch(CityIdPre, CateIdPre, listDistrictIds, listContentIds, "", numberDefault, pageIndexDefault);
        });
    }
    //end click remove keyword

    //load store
    function updateListStoreSearch(CityID, CateID, DistrictIDs, ContentIDs, KeyWord, NumberOfItem, PageIndex) {
        $.ajax({
            url: '/Search/ListStoreSearch',
            type: 'POST',
            data: {
                CityID: CityID,
                CateID: CateID,
                Districts: DistrictIDs,
                Contents: ContentIDs,
                KeyWord: KeyWord,
                NumberOfItem: NumberOfItem,
                PageIndex: PageIndex
            },
            success: function (data) {
                $('.list-store-search').html(data);
                updatePaginationListStore(NumberOfItem, PageIndex);
            }
        });
    }
    //end load store

    //add paging
    function updatePaginationListStore(numberOfItem, pageIndex) {
        $('.pagination').empty();

        $.ajax({
            url: '/Service/ListStoreSearch',
            type: 'POST',
            data: {
                CityID: CityIdPre,
                CateID: CateIdPre,
                Districts: listDistrictIds,
                Contents: listContentIds,
                KeyWord: keyWordInPut
            },
            success: function (data) {
                if (data == null || (data.length / numberOfItem) < 1) {
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
                                updateListStoreSearch(CityIdPre, CateIdPre, listDistrictIds, listContentIds, keyWordInPut, numberOfItem, i);
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
    //end add paging
});