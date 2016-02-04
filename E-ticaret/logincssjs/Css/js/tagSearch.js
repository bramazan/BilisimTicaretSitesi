var dogru;
function convertToSeo5(name) { var seosectionname = name; seosectionname = seosectionname.replace("ç", "c"); seosectionname = seosectionname.replace("ğ", "g"); seosectionname = seosectionname.replace("ı", "i"); seosectionname = seosectionname.replace("ö", "o"); seosectionname = seosectionname.replace("ş", "s"); seosectionname = seosectionname.replace("ü", "u"); seosectionname = seosectionname.replace("Ç", "c"); seosectionname = seosectionname.replace("Ğ", "g"); seosectionname = seosectionname.replace("İ", "i"); seosectionname = seosectionname.replace("I", "i"); seosectionname = seosectionname.replace("Ö", "o"); seosectionname = seosectionname.replace("O", "o"); seosectionname = seosectionname.replace("Ş", "s"); seosectionname = seosectionname.replace("Ü", "u"); seosectionname = seosectionname.replace("U", "u"); seosectionname = seosectionname.replace(/\s/g, '-'); seosectionname = seosectionname.toLowerCase(); return seosectionname; }
$(document).ready(function () {
    $(".txtTags").keyup(function () {
        var kelime = $(this).val();
        var aLar = "";
        var content = "";
        var id = $(".aramadrop").val();
        if (kelime != "" && kelime.length >= 1) {
            $.ajax({
                type: "GET",
                url: "/searchResults.aspx",
                //data: "{'kelime':'" + kelime + "','kat_id':'" + id + "'}",
                data: "kelime=" + kelime + "&kat_id=" + id + "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (data) {
                    if (data.kod == 100) {
                        console.log(data);
                        $.each(data.sonuclar, function () {
                            content += "<div class='satir'><a href='/urun-detay/" + convertToSeo5(this.kat_adi) + "/" + convertToSeo5(this.urun_adi) + "-u-" + this.id + "'>";
                            content += "<div class='item'>";
                            content += "<div class='itemimg'><img Width='80' Height='80' src='/uploads/urunler_img/100x100/" + this.img_path + "'/></div>";
                            content += "<div class='itemcontent'>";
                            content += "<h2>" + this.urun_adi + "</h2>";
                            content += "<p>" + this.kisa_aciklama + "</p>";
                            content += "</div></div></a></div>";
                        });
                        $('.PrSearchResult').css('display', 'block');
                        $(".PrSearchResult").html('').html(content).css("display", "block !important");
                    }
                    else {
                        $(".PrSearchResult").text("Aradığınız kelimeyle sonuç bulunamadı.").css("display", "block").css("color", "BF72C5");
                    }

                }, error: function (val) {
                    alert("Hata");
                }
            });
        }
        else {
            $(".PrSearchResult").html('').css("display", "none");
            $(".PrSearchResult").html('').text("Lütfen kategori seçtikten sonra harf giriniz").css("display", "block");
        }
        $("body").not($(".PrSearchResult")).click(function () {

            $(".PrSearchResult").hide();

        });

    });
})