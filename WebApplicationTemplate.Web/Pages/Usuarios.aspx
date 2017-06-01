<%@ Page Language="C#"  MasterPageFile="~/Masters/PrivatePage.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.Usuarios"  validateRequest="false" enableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <div class="container" style="width: 100%">
        <div class="signup-form-container" style="width: 100%">
            <form  id="register-form" action="#" method="post" role="form" autocomplete="off">

                <asp:updatepanel runat="server" ID="udpEtiquetas" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="alert alert-success" runat="server" id="lblUsuario" visible="false"></div>
                        <div class="alert alert-danger" runat="server" id="lblUsuarioEliminado" visible="false"></div>
                        <div class="alert alert-danger" runat="server" id="lblError" visible="false"></div>
                    </ContentTemplate>
                </asp:updatepanel>

                <div class="form-header">
                    <h3 class="form-title"><i class="fa fa-user"></i> Registro de usuario</h3>
                </div>
                
                <div class="form-body">

                    <asp:UpdatePanel runat="server" ID="udpUsername" UpdateMode="Always">
                        <ContentTemplate>

                            <asp:HiddenField runat="server" ID="hdnIdUsuario" Value="" />

                            <div class="form-group">
                                <div class="input-group">
                                <div class="input-group-addon"><span class="glyphicon glyphicon-user"></span></div>
                                <input name="username" value="" id="username" runat="server" type="text" class="form-control" placeholder="Nombre de usuario" maxlength="50" />
                                </div>
                                <span class="help-block" id="error"></span>
                            </div>
                        
                            <div class="form-group">
                                <div class="input-group">
                                <div class="input-group-addon"><span class="glyphicon glyphicon-user"></span></div>
                                <input name="nombre" id="nombre" runat="server" type="text" class="form-control" placeholder="Nombre(s)" maxlength="50"/>
                                </div>
                                <span class="help-block" id="error"></span>
                            </div>

                            <div class="row">
                                            
                                <div class="form-group col-lg-6">
                                    <div class="input-group">
                                    <div class="input-group-addon"><span class="glyphicon glyphicon-user"></span></div>
                                    <input name="paterno" id="paterno" runat="server" class="form-control" placeholder="Apellido Paterno" maxlength="50"/>
                                    </div>  
                                    <span class="help-block" id="error"></span>                    
                                </div>
                            
                                <div class="form-group col-lg-6">
                                    <div class="input-group">
                                    <div class="input-group-addon"><span class="glyphicon glyphicon-user"></span></div>
                                    <input name="materno" id="materno" runat="server" class="form-control" placeholder="Apellido Materno" maxlength="50"/>
                                    </div>  
                                    <span class="help-block" id="error"></span>                    
                                </div>
                            
                             </div>

                            <div class="form-group">
                                <div class="input-group">
                                <div class="input-group-addon"><span class="glyphicon glyphicon-envelope"></span></div>
                                <input name="email" id="email" runat="server" type="text" class="form-control" placeholder="Email" maxlength="50"/>
                                </div> 
                                <span class="help-block" id="error"></span>                     
                            </div>

                            <div class="row">
                                            
                                <div class="form-group col-lg-6">
                                    <div class="input-group">
                                    <div class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></div>
                                    <input name="password" id="password" runat="server" type="password" class="form-control" placeholder="Contraseña" maxlength="15"/>
                                    <input name="password" id="passwordEdit" visible="false" runat="server" type="text" class="form-control" placeholder="Contraseña" maxlength="15"/>
                                    </div>  
                                    <span class="help-block" id="error"></span>                    
                                </div>
                            
                                <div class="form-group col-lg-6">
                                    <div class="input-group">
                                    <div class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></div>
                                    <input name="cpassword" id="cpassword" runat="server" type="password" class="form-control" placeholder="Confirmar Contraseña" maxlength="15"/>
                                    <input name="cpassword" id="cpasswordEdit" visible="false" runat="server" type="text" class="form-control" placeholder="Confirmar Contraseña" maxlength="15"/>
                                    </div>  
                                    <span class="help-block" id="error"></span>                    
                                </div>

                            </div>

                            <div class="form-group" style="width: 30%">
                                <div class="input-group">
                                  <span class="input-group-addon">
                                    <input name="superuser" id="superuser" class="check" runat="server" type="checkbox"/>
                                  </span>
                                  <input type="text" class="form-control" readonly="readonly" value="Administrador"/>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="form-footer" style="width: 40%">

                        <table>
                            <tr>
                                <td>
                                    <asp:Button id="btnGuardar" CssClass="btn btn-default" runat="server" Text="Guardar"
                                        OnClick="btnGuardar_Click"/>
                                </td>
                                <td>&nbsp &nbsp</td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="udpBtnCancelarEdicion" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnCancelarEdicion" Text="Cancelar Edición"
                                            CssClass="btn btn-default" Visible="false" OnClick="btnCancelarEdicion_Click"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>&nbsp &nbsp</td>
                                <td>
                                    <input id="btnLimpiarCampos" class="btn btn-default" value="Limpiar" type="button"
                                        onclick="limpiaForm(this)"/>
                                </td>
                            </tr>
                        </table>
                        
                    </div>

                </div>
            </form>
        </div>
    </div>

    <br />
    
    <asp:UpdatePanel ID="udpGrdUsuarios" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">
        <div class="row">
            <div class="col-md-12">
                <h2></h2>
                <h3 class="form-title"><i class="fa fa-user"></i> Usuarios</h3>
            </div>
        </div>
        
        <asp:GridView runat="server" ID="grdUsuarios" 
            AutoGenerateColumns="false" 
            CssClass="table table-bordered" 
            OnRowCancelingEdit="grdUsuarios_RowCancelingEdit" 
            OnRowDeleting="grdUsuarios_RowDeleting"
            OnRowEditing="grdUsuarios_RowEditing"
            OnRowUpdating="grdUsuarios_RowUpdating"
            ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:BoundField DataField="IdUser" HeaderText="ID" ReadOnly="true" ItemStyle-Width="5%" ControlStyle-CssClass="form-control"/>
                <asp:TemplateField HeaderText="Nombre de Usuario" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnUsername" runat="server" Value='<%#Bind("Username") %>' />
                        <asp:Label ID="lblUsername" runat="server" Text='<%#Bind("Username") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Nombre">
                    <ItemTemplate>
                        <div class="input-group">
                            <span>&nbsp;</span>
                            <asp:Label ID="lblNombre" runat="server" Text='<%#Bind("Nombre") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Apellido Paterno">
                    <ItemTemplate>
                        <div class="input-group">
                            <span>&nbsp;</span>
                            <asp:Label ID="lblApellidoPaterno" runat="server" Text='<%#Bind("ApellidoPaterno") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Apellido Materno">
                    <ItemTemplate>
                        <div class="input-group">
                            <span>&nbsp;</span>
                            <asp:Label ID="lblApellidoMaterno" runat="server" Text='<%#Bind("ApellidoMaterno") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Email">
                    <ItemTemplate>
                        <div class="input-group">
                            <span>&nbsp;</span>
                            <asp:Label ID="lblEmail" runat="server" Text='<%#Bind("Email") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Administrador">
                    <ItemTemplate>
                        <div class="input-group">
                            <span>&nbsp;</span>
                            <asp:CheckBox ID="chkIsSuperUser" runat="server" Checked='<%#Bind("IsSuperUser") %>' Enabled="false" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="true" ItemStyle-Width="20%" />
                <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="10%" />
            </Columns>
        </asp:GridView>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    

    <script type="text/javascript">

        jQuery(document).ready(function () {
            jQuery('#Content_username').keypress(function (tecla) {
                if (tecla.charCode == 32) return false;
            });
        });

        function limpiaForm() {
            $('#Content_username').val('');
            $('#Content_username').closest('.form-group').removeClass('has-error').removeClass('has-success');
            $('#Content_username').closest('.form-group').find('.help-block').html('');

            $('#Content_nombre').val('');
            $('#Content_nombre').closest('.form-group').removeClass('has-error').removeClass('has-success');
            $('#Content_nombre').closest('.form-group').find('.help-block').html('');

            $('#Content_paterno').val('');
            $('#Content_paterno').closest('.form-group').removeClass('has-error').removeClass('has-success');
            $('#Content_paterno').closest('.form-group').find('.help-block').html('');

            $('#Content_materno').val('');
            $('#Content_materno').closest('.form-group').removeClass('has-error').removeClass('has-success');
            $('#Content_materno').closest('.form-group').find('.help-block').html('');

            $('#Content_email').val('');
            $('#Content_email').closest('.form-group').removeClass('has-error').removeClass('has-success');
            $('#Content_email').closest('.form-group').find('.help-block').html('');

            $('#Content_password').val('');
            $('#Content_password').closest('.form-group').removeClass('has-error').removeClass('has-success');
            $('#Content_password').closest('.form-group').find('.help-block').html('');

            $('#Content_cpassword').val('');
            $('#Content_cpassword').closest('.form-group').removeClass('has-error').removeClass('has-success');
            $('#Content_cpassword').closest('.form-group').find('.help-block').html('');

            $(".check").each(function () {
                $(this).prop('checked', false);
            });
            $('#Content_superuser').closest('.form-group').removeClass('has-error').removeClass('has-success');
        }

        $('document').ready(function () {

            // username validation
            var usernameregex = /^[a-zA-Z0-9]+$/;

            $.validator.addMethod("validusername", function (value, element) {
                return this.optional(element) || usernameregex.test(value);
            });

            // name validation
            var nameregex = /^[a-zA-Z ]+$/;

            $.validator.addMethod("validname", function (value, element) {
                return this.optional(element) || nameregex.test(value);
            });

            // valid email pattern
            var eregex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            $.validator.addMethod("validemail", function (value, element) {
                return this.optional(element) || eregex.test(value);
            });
            
            $("#frm").validate({

                rules:
                {
                    ctl00$Content$username: {
                        required: true,
                        minlength: 4,
                        maxlength: 50,
                        validusername: true
                    },
                    ctl00$Content$nombre: {
                        required: true,
                        validname: true,
                        minlength: 4,
                        maxlength: 50
                    },
                    ctl00$Content$paterno: {
                        required: true,
                        validname: true,
                        minlength: 4,
                        maxlength: 50
                    },
                    ctl00$Content$materno: {
                        required: true,
                        validname: true,
                        minlength: 4,
                        maxlength: 50
                    },
                    ctl00$Content$email: {
                        required: true,
                        validemail: true,
                        maxlength: 50
                    },
                    ctl00$Content$password: {
                        required: true,
                        minlength: 8,
                        maxlength: 15
                    },
                    ctl00$Content$cpassword: {
                        required: true,
                        minlength: 8,
                        maxlength: 15,
                        equalTo: '#Content_password'
                    },
                },
                messages:
                {
                    ctl00$Content$username: {
                        required: "Por favor ingrese su nombre de usuario.",
                        minlength: "Su nombre de usuario es muy corto",
                        maxlength: "No se permiten más de 50 caracteres.",
                        validusername: "No se permiten espacios."
                    },
                    ctl00$Content$nombre: {
                        required: "Por favor ingrese su nombre.",
                        validname: "El nombre debe de contener solo letras.",
                        minlength: "Su nombre es muy corto.",
                        maxlength: "No se permiten más de 50 caracteres."
                    },
                    ctl00$Content$paterno: {
                        required: "Por favor ingrese su apellido paterno.",
                        validname: "El apellido debe de contener solo letras.",
                        minlength: "El apellido es muy corto.",
                        maxlength: "No se permiten más de 50 caracteres."
                    },
                    ctl00$Content$materno: {
                        required: "Por favor ingrese su apellido materno.",
                        validname: "El apellido debe de contener solo letras.",
                        minlength: "El apellido es muy corto.",
                        maxlength: "No se permiten más de 50 caracteres."
                    },
                    ctl00$Content$email: {
                        required: "Por favor ingrese su dirección de correo electrónico.",
                        validemail: "Ingrese una dirección de correo valida.",
                        maxlength: "No se permiten más de 50 caracteres."
                    },
                    ctl00$Content$password: {
                        required: "Por favor introduzca su contraseña.",
                        minlength: "La contraseña debe tener al menos 8 caracteres.",
                        maxlength: "La contraseña no debe tener más de 15 caracteres"
                    },
                    ctl00$Content$cpassword: {
                        required: "Por favor confirme su contraseña.",
                        minlength: "La contraseña debe tener al menos 8 caracteres.",
                        maxlength: "La contraseña no debe tener más de 15 caracteres",
                        equalTo: "La contraseña no coincide."
                    }
                },
                errorPlacement: function (error, element) {
                    $(element).closest('.form-group').find('.help-block').html(error.html());
                },
                highlight: function (element) {
                    $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
                },
                unhighlight: function (element, errorClass, validClass) {
                    $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
                    $(element).closest('.form-group').find('.help-block').html('');
                },

                submitHandler: function (form) {
                    form.submit();
                }
            });
        })

    </script>

</asp:Content>