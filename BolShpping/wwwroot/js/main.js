(function($) {
    "use strict";

    /*--
        01. WOW active 
    ------------------------- */
    new WOW().init();
    /*--
        02. stickey menu
    ------------------------- */
    $(window).on('scroll', function() {
        var scroll = $(window).scrollTop();
        if (scroll < 500) {
            $(".sticky-header").removeClass("sticky");
        } else {
            $(".sticky-header").addClass("sticky");
        }
    });

    /*--
        03. jQuery MeanMenu
    ------------------------- */
    $('#mobile-menu-active').meanmenu({
        meanScreenWidth: "991",
        meanMenuContainer: ".mobile-menu-area .mobile-menu",
    });

    /*--
    	04. Header Top Dropdown Menu 
    -----------------------------------*/
    $('.drodown-show > a').on('click', function(e) {
        e.preventDefault();
        if ($(this).hasClass('active')) {
            $('.drodown-show > a').removeClass('active').siblings('.open-dropdown').slideUp()
            $(this).removeClass('active').siblings('.open-dropdown').slideUp();
        } else {
            $('.drodown-show > a').removeClass('active').siblings('.open-dropdown').slideUp()
            $(this).addClass('active').siblings('.open-dropdown').slideDown();
        }
    });

    /*--
        05. Nice Select
    ------------------------- */
    $('.nice-select').niceSelect();


    /*--
        06. owl active
    --------------------------- */
    $('.slider-active').owlCarousel({
        loop: true,
        items: 1,
        autoplay: false,
        dots: false,
        nav: true,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            },
            1200: {
                items: 1
            }
        }
    });
    /*--
        owl active
    ------------------------------ */
    $('.product-active').owlCarousel({
        loop: true,
        items: 3,
        dots: false,
        nav: false,
        navText: ['<i class="ion-chevron-left"></i>', '<i class="ion-chevron-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            },
            1200: {
                items: 3
            }
        }
    });

    /*--
        owl active
    ------------------------------ */
    $('.product-active-2').owlCarousel({
        loop: true,
        items: 1,
        dots: false,
        nav: true,
        navText: ['<i class="ion-chevron-left"></i>', '<i class="ion-chevron-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1
            },
            768: {
                items: 2
            },
            992: {
                items: 2
            },
            1200: {
                items: 2
            }
        }
    });
    /*--
        owl active
    ------------------------------ */
    $('.product-active-3').owlCarousel({
        loop: true,
        items: 1,
        dots: false,
        nav: true,
        navText: ['<i class="ion-chevron-left"></i>', '<i class="ion-chevron-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            },
            1200: {
                items: 4
            }
        }
    });

    /*--
        owl active
    ------------------------------ */
    $('.our-brand-active').owlCarousel({
        loop: true,
        items: 1,
        dots: false,
        nav: false,
        responsive: {
            0: {
                items: 2
            },
            480: {
                items: 3
            },
            768: {
                items: 4
            },
            992: {
                items: 5
            },
            1200: {
                items: 5
            }
        }
    });
    /*--
        owl active
    ------------------------------ */
    $('.testimonials-item').owlCarousel({
        loop: true,
        items: 1,
        autoplay: true,
        dots: true,
        nav: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            },
            1200: {
                items: 1
            }
        }
    });
    /*--
        owl active
    ------------------------------ */
    $('.latest-blog-active').owlCarousel({
        loop: true,
        items: 1,
        dots: false,
        nav: true,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1
            },
            768: {
                items: 2
            },
            992: {
                items: 2
            },
            1200: {
                items: 2
            }
        }
    });
    /*--
        owl active
    ------------------------------ */
    $('.latest-blog-active-2').owlCarousel({
        loop: true,
        items: 1,
        dots: false,
        nav: true,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            },
            1200: {
                items: 4
            }
        }
    });
    /*--
        owl active
    ------------------------------ */
    $('.category-item-active').owlCarousel({
        loop: true,
        items: 1,
        margin: 30,
        dots: false,
        nav: true,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1
            },
            768: {
                items: 2
            },
            992: {
                items: 1
            },
            1200: {
                items: 1
            }
        }
    });

    /*--
        owl active
    --------------------------- */
    $('.related-product-active').owlCarousel({
        loop: true,
        items: 1,
        autoplay: false,
        dots: false,
        nav: false,
        margin: 10,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 4
            },
            1200: {
                items: 4
            }
        }
    });

    /*--
        owl active
    ------------------------------ */
    $('.post-slider').owlCarousel({
        loop: true,
        items: 1,
        dots: true,
        nav: true,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            },
            1200: {
                items: 1
            }
        }
    });
    /*--
        owl active
    ------------------------------- */
    $('.testimonials-active').owlCarousel({
        loop: true,
        items: 1,
        dots: false,
        nav: false,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            },
            1200: {
                items: 1
            }
        }
    });
    /*--
    	07. slick  slider
    ----------------------------*/

    $('.slider-vertical').slick({
        slidesToShow: 4,
        autoplay: false,
        vertical: true,
        verticalSwiping: true,
        slidesToScroll: 1,
        prevArrow: '<i class="fa fa-angle-right arrow-prv"></i>',
        nextArrow: '<i class="fa fa-angle-left arrow-next"></i>',
        button: false,
        responsive: [{
                breakpoint: 1024,
                settings: {
                    slidesToShow: 4,
                }
            },
            {
                breakpoint: 991,
                settings: {
                    slidesToShow: 3,
                    vertical: false,
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 3,
                    vertical: false,
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 2,
                    vertical: false,
                }
            }
        ]

    });
    $('.slider-vertical a').on('click', function() {
        $('.slider-vertical a').removeClass('active');
    });

    /*--
        08. Cart Plus Minus Button
    ---------------------------------*/
    $(".cart-plus-minus").append('<div class="dec qtybutton"><i class="fa fa-angle-down"></i></div><div class="inc qtybutton"><i class="fa fa-angle-up"></i></div>');
    $(".qtybutton").on("click", function() {
        var $button = $(this);
        var oldValue = $button.parent().find("input").val();
        if ($button.hasClass('inc')) {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            // Don't allow decrementing below zero
            if (oldValue > 0) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 0;
            }
        }
        $button.parent().find("input").val(newVal);
    });




    /*--
       09. elevateZoom
    ------------------------------ */
    $("#zoom1").elevateZoom({
        gallery: 'gallery_01',
        responsive: true,
        zoomType: 'inner',
        cursor: 'crosshair'
    });

    /*--
        10. Category menu Activation
    ------------------------------*/
    $('.category-sub-menu li.has-sub > a').on('click', function() {
        $(this).removeAttr('href');
        var element = $(this).parent('li');
        if (element.hasClass('open')) {
            element.removeClass('open');
            element.find('li').removeClass('open');
            element.find('ul').slideUp();
        } else {
            element.addClass('open');
            element.children('ul').slideDown();
            element.siblings('li').children('ul').slideUp();
            element.siblings('li').removeClass('open');
            element.siblings('li').find('li').removeClass('open');
            element.siblings('li').find('ul').slideUp();
        }
    });

    /*--
    	11. Count Down Timer
    ----------------------------*/
    $('[data-countdown]').each(function() {
        var $this = $(this),
            finalDate = $(this).data('countdown');
        $this.countdown(finalDate, function(event) {
            $this.html(event.strftime('<span class="cdown day"><span class="time-count">%-D</span> <p>Days</p></span> <span class="cdown hour"><span class="time-count">%-H</span> <p>Hours</p></span> <span class="cdown minutes"><span class="time-count">%M</span> <p>mins</p></span> <span class="cdown second"><span class="time-count">%S</span> <p>secs</p></span>'));
        });
    });

    /*--
        13. Category menu Activation
    ------------------------------*/
    $('#cate-toggle li.has-sub>a,#cate-mobile-toggle li.has-sub>a').on('click', function() {
        $(this).removeAttr('href');
        var element = $(this).parent('li');
        if (element.hasClass('open')) {
            element.removeClass('open');
            element.find('li').removeClass('open');
            element.find('ul').slideUp();
        } else {
            element.addClass('open');
            element.children('ul').slideDown();
            element.siblings('li').children('ul').slideUp();
            element.siblings('li').removeClass('open');
            element.siblings('li').find('li').removeClass('open');
            element.siblings('li').find('ul').slideUp();
        }
    });

    /*--
        14. Accordion
    -------------------------*/
    $(".faequently-accordion").collapse({
        accordion: true,
        open: function() {
            this.slideDown(300);
        },
        close: function() {
            this.slideUp(300);
        }
    });

    /*--
        showlogin toggle function
    --------------------------*/
    $('#showlogin').on('click', function() {
        $('#checkout-login').slideToggle(500);
    });

    /*--
        showcoupon toggle function
    --------------------------*/
    $('#showcoupon').on('click', function() {
        $('#checkout-coupon').slideToggle(500);
    });

    /*--
        Checkout 
    --------------------------*/
    $("#chekout-box").on("change", function() {
        $(".account-create").slideToggle("100");
    });

    /*-- 
        Checkout 
    ---------------------------*/
    $("#chekout-box-2").on("change", function() {
        $(".ship-box-info").slideToggle("100");
    });

    /*--
        15. ScrollUp Active
    -----------------------------------*/
    $.scrollUp({
        scrollText: '<i class="fa fa-angle-double-up"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    });





})(jQuery);