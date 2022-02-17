var btn = document.getElementById("calc");
var age = document.getElementById("age");
var height = document.getElementById("height");
var weight = document.getElementById("weight");
var select = document.getElementById("select");
var calories;
var result = document.getElementById("result");
var reduce = document.getElementById("reduce");
var reduce2 = document.getElementById("reduce2");



btn.addEventListener('click',function(e){
    e.preventDefault();
    
    if (select.value == 0) {
        calories=Number( weight.value) * 24 * 1.3;
    } else if(select.value == 1){
        calories=Number( weight.value) * 24 * 1.4;
    }
    else{
        calories=Number( weight.value) * 24 * 1.5;
    }

    result.style.display="block";
    result.innerText=" عدد السعرات الحراريةالتى يحتاجها الجسم = " + parseInt(calories) +" "+ "سعر حراري ";
    reduce.style.display="block";
    reduce2.style.display="block";
})

