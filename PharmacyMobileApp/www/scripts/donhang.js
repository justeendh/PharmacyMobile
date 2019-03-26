var app = new Vue({
  el: "#main",
  data: {
      listOrders: [],
      pageCount: 0,
      currentPage: 1
  },
  mounted() {
      this.loadData();
  },
  updated() {
    $('.formatnumber').number( true, 0, ',', '.' );
  },
  methods: {
      loadData: function () {
          let urlGetData = HOST_DATA + "Pharmacy/GetMyCart?page=" + this.currentPage;
          let moduleInstance = this;
          $.ajax({
              async: false,
              type: "GET",
              url: urlGetData,
              contentType: "application/json",
              success: function (response) {
                  if (response.success === true) {
                      moduleInstance.listOrders = response.result;
                      moduleInstance.pageCount = response.totalPage;
                      moduleInstance.currentPage = response.currentPage
                  }
              }
          });
      },
      changePage: function () {
          this.loadData();
      }
  }
});
