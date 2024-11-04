
// click hidden list city & list option user
$(document).click(function (event) {
    var cityname = $('.header-city>ul');
    if (!cityname.is(event.target) && cityname.has(event.target).length === 0) {
        cityname.hide();
    }

    var listoptionUser = $('.header-user-login>ul');
    if (!listoptionUser.is(event.target) && listoptionUser.has(event.target).length === 0) {
        listoptionUser.hide();
    }
});
//end click hidden list city & list option user

$(document).ready(function () {
    let InfoHeader = JSON.parse(localStorage.getItem('Info_Header'))
    if (InfoHeader == null) {
        SetInfoHeader(1, 1);
    }
    SetHeader();

    function SetInfoHeader(CityId, CateId) {
        let InfoHeader = {
            CityIdHeader: CityId,
            CateIdHeader: CateId
        };
        localStorage.setItem('Info_Header', JSON.stringify(InfoHeader))
    }

    // list city in header
    $('.header-city-name').click(function (event) {
        event.stopPropagation();
        var mainHeader = $(this).closest('.main-header');
        var cityname = mainHeader.find('.header-city>ul');
        cityname.toggle();
    });

    $('.name-city').click(function () {
        var CityID = $(this).attr('cityId');
        var cityName = $(this).text();
        $('.header-city-name').text(cityName);
        $('.header-city-name').attr('cityId', CityID);
        var CateID = $('.header-category-option li.active').attr('CateId');

        ListFunctionGeneric(CityID, CateID, defaultDistirctID, NumberOfItemDefault);
    });
    // end click city in header

    // category-header
    var categoryItems = $('.header-category-option li');
    if (categoryItems.filter('.active').length === 0) {
        categoryItems.first().addClass('active');
    }

    $('.header-category-option li').click(function () {
        $('.header-category-option li.active').removeClass('active');
        $(this).addClass('active');

        var CateID = $(this).attr('CateId');
        var CityID = $('.header-city-name').attr('cityId');

        ListFunctionGeneric(CityID, CateID, defaultDistirctID, NumberOfItemDefault);
    });
    //end category-header

    //defaultPageHome
    var defaultCityId = $('.header-city-name').attr('cityId');
    var defaultCateId = $('.header-category-option li.active').attr('CateId');
    var defaultDistirctID = $('.list-district-menubot').val();
    const NumberOfItemDefault = 6;

    ListFunctionGeneric(defaultCityId, defaultCateId, defaultDistirctID, NumberOfItemDefault)
    //end defaultPageHome

    //update list preferential home
    function updatePreferentialHome(CityID, CateID, NumberOfItem) {
        $.ajax({
            url: '/Home/ListPreferentialHome',
            type: 'POST',
            data: {
                CityID: CityID,
                CateID: CateID,
                NumberOfItem: NumberOfItem,
                PageIndex: 1
            },
            success: function (data) {
                if (data == 0) {
                    $("#list-store-pre").hide();
                }
                $('.preferentialHome').html(data);
                var itemCount = document.querySelectorAll('.home-store-preferential').length;
                if (itemCount < NumberOfItem) {
                    $('#select-more-preferential').hide();
                } else {
                    $('#select-more-preferential').show();
                }
            }
        });
    }
    // end update list preferential home

    //list collection home
    function updateCollectionHome(CityID, CateID) {
        $.ajax({
            url: '/Home/ListCollectionHome',
            type: 'POST',
            data: {
                CityID: CityID,
                CateID: CateID,
                NumberOfItem: 6,
                PageIndex: 1
            },
            success: function (data) {
                $('.collectionHome').html(data);
                if (document.querySelectorAll(".data-empty").length == 1) {
                    $("#list-collection-home").hide();
                }
            }
        });
    }
    // end list collection home

    //click see more
    $('#select-more-preferential').click(function () {
        let InfoHeader = JSON.parse(localStorage.getItem('Info_Header'))
        CityID = InfoHeader.CityIdHeader;
        CateID = InfoHeader.CateIdHeader;
        seeMorePre += 6;
        updatePreferentialHome(CityID, CateID, seeMorePre);
    });
    //end click see more

    //list option home
    function updateListOption(CateID) {
        $.ajax({
            url: '/Home/ListOptionHome',
            type: 'GET',
            data: {
                CateID: CateID,
            },
            success: function (data) {
                $('.home-listoption').html(data);
            }
        });
    }
    // end list option home

    // list user in header
    $('.header-user-name').click(function (event) {
        event.stopPropagation();
        var mainUserLogin = $(".header-user-login");
        var listoptionUser = mainUserLogin.find('.user-login-menu');
        listoptionUser.toggle();
    });
    //end user city in header

    //list district
    function LoadListDistrict(CityID) {
        $.ajax({
            url: '/Home/ListDistrictMenuBot',
            type: 'Get',
            data: {
                CityID: CityID,
            },
            success: function (data) {
                $('.list-district-menubot').html(data);
            }
        });
    };
    //end list district

    //update list store menu bot home
    function listStoreMenuBot(CityID, CateID, DistrictID, NumberOfItem) {
        $.ajax({
            url: '/Home/ListStoreMenuBot',
            type: 'POST',
            data: {
                CityID: CityID,
                CateID: CateID,
                DistrictID: DistrictID,
                NumberOfItem: NumberOfItem,
                PageIndex: 1
            },
            success: function (data) {
                $('.list-store-menubot').html(data);
                var itemCount = document.querySelectorAll('.store-default').length;
                if (itemCount < NumberOfItem) {
                    $('#select-more-menubot').hide();
                } else {
                    $('#select-more-menubot').show();
                }
            }
        });
    }
    // end update list store menu bot home

    //load list district in menu bot
    $(".list-district-menubot").on("change", function () {
        var DistrictID = $(this).val();
        let InfoHeader = JSON.parse(localStorage.getItem('Info_Header'))
        CityID = InfoHeader.CityIdHeader;
        CateID = InfoHeader.CateIdHeader;
        listStoreMenuBot(CityID, CateID, DistrictID, NumberOfItemDefault);

    });
    //end load list district in menu bot

    //click see more menu bot
    $('#select-more-menubot').click(function () {
        let InfoHeader = JSON.parse(localStorage.getItem('Info_Header'))
        CityID = InfoHeader.CityIdHeader;
        CateID = InfoHeader.CateIdHeader;
        DistrictID = $('.list-district-menubot').val();
        seeMoreBot += 6;
        listStoreMenuBot(CityID, CateID, DistrictID, seeMoreBot);
    });
    //end click see more menu bot

    //syn all function generic
    function ListFunctionGeneric(CityID, CateID, DistrictID, NumberOfItem) {
        updatePreferentialHome(CityID, CateID, NumberOfItem)
        updateCollectionHome(CityID, CateID);
        updateListOption(CateID);
        SetInfoHeader(CityID, CateID)
        LoadListDistrict(CityID);
        listStoreMenuBot(CityID, CateID, DistrictID, NumberOfItem)
        seeMorePre = 6;
        seeMoreBot = 6;
    }
    //end syn all function generic

    //set header
    function SetHeader() {
        let InfoHeader = JSON.parse(localStorage.getItem('Info_Header'));
        var CityIdPre = InfoHeader.CityIdHeader;
        var CateIdPre = InfoHeader.CateIdHeader;

        var cityHeader = document.querySelectorAll('.header-city-name-option li');
        cityHeader.forEach(function (item) {
            if (item.getAttribute('cityId') == CityIdPre) {
                var cityName = item.textContent;
                $('.header-city-name').text(cityName);
                $('.header-city-name').attr("cityId", CityIdPre);
            } else {
                item.addEventListener('click', function () {
                    window.location.href = "/";
                });
            }
        });

        var categoryHeader = document.querySelectorAll('.header-category-option li');
        categoryHeader.forEach(function (item) {
            if (item.getAttribute('CateId') == CateIdPre) {
                item.classList.add('active');
            } else {
                item.addEventListener('click', function () {
                    window.location.href = "/";
                });
            }
        });
    }

    // end set header
});
window.addEventListener("load", () => {
    const loader = document.querySelector(".loader");

    loader.classList.add("loader--hidden");

    loader.addEventListener("transitionend", () => {
        document.body.removeChild(loader);
    });
});