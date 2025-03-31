using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UberApi.Migrations
{
    /// <inheritdoc />
    public partial class CreationBDUberApiTD1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "adresse_id_seq");

            migrationBuilder.CreateSequence(
                name: "carte_bancaire_id_seq");

            migrationBuilder.CreateSequence(
                name: "categorie_prestation_id_seq");

            migrationBuilder.CreateSequence(
                name: "categorie_produit_id_seq");

            migrationBuilder.CreateSequence(
                name: "client_id_seq");

            migrationBuilder.CreateSequence(
                name: "code_postal_id_seq");

            migrationBuilder.CreateSequence(
                name: "commande_id_seq");

            migrationBuilder.CreateSequence(
                name: "course_id_seq");

            migrationBuilder.CreateSequence(
                name: "coursier_id_seq");

            migrationBuilder.CreateSequence(
                name: "entreprise_id_seq");

            migrationBuilder.CreateSequence(
                name: "entretien_id_seq");

            migrationBuilder.CreateSequence(
                name: "etablissement_id_seq");

            migrationBuilder.CreateSequence(
                name: "facture_id_seq");

            migrationBuilder.CreateSequence(
                name: "gestion_etablissement_id_seq");

            migrationBuilder.CreateSequence(
                name: "horaires_id_seq");

            migrationBuilder.CreateSequence(
                name: "lieu_favori_id_seq");

            migrationBuilder.CreateSequence(
                name: "livreur_id_seq");

            migrationBuilder.CreateSequence(
                name: "otp_id_seq");

            migrationBuilder.CreateSequence(
                name: "panier_id_seq");

            migrationBuilder.CreateSequence(
                name: "pays_id_seq");

            migrationBuilder.CreateSequence(
                name: "planning_reservation_id_seq");

            migrationBuilder.CreateSequence(
                name: "produit_id_seq");

            migrationBuilder.CreateSequence(
                name: "reglement_salaire_id_seq");

            migrationBuilder.CreateSequence(
                name: "reservation_id_seq");

            migrationBuilder.CreateSequence(
                name: "responsable_enseigne_id_seq");

            migrationBuilder.CreateSequence(
                name: "restaurateur_id_seq");

            migrationBuilder.CreateSequence(
                name: "type_prestation_id_seq");

            migrationBuilder.CreateSequence(
                name: "vehicule_id_seq");

            migrationBuilder.CreateSequence(
                name: "velo_id_seq");

            migrationBuilder.CreateSequence(
                name: "velo_reservation_id_seq");

            migrationBuilder.CreateSequence(
                name: "ville_id_seq");

            migrationBuilder.CreateTable(
                name: "t_e_cartebancaire_ctbr",
                columns: table => new
                {
                    cb_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('carte_bancaire_id_seq'::regclass)"),
                    cb_numero = table.Column<string>(type: "text", nullable: false),
                    cb_dateexpire = table.Column<DateOnly>(type: "date", nullable: false),
                    cb_cryptogramme = table.Column<string>(type: "text", nullable: false),
                    cb_typecarte = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    cb_typereseaux = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_carte_bancaire", x => x.cb_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_categorieprestation_cpr",
                columns: table => new
                {
                    cpr_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('categorie_prestation_id_seq'::regclass)"),
                    cpr_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    cpr_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    cpr_image = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categorie_prestation", x => x.cpr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_categorieproduit_cpt",
                columns: table => new
                {
                    cpt_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('categorie_produit_id_seq'::regclass)"),
                    cpt_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categorie_produit", x => x.cpt_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_livreur_livr",
                columns: table => new
                {
                    livr_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('livreur_id_seq'::regclass)"),
                    ent_id = table.Column<int>(type: "integer", nullable: false),
                    adr_id = table.Column<int>(type: "integer", nullable: true),
                    livr_genreuser = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    livr_nomuser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    livr_prenomuser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    livr_datenaissance = table.Column<DateOnly>(type: "date", nullable: false),
                    livr_telephone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    livr_emailuser = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    livr_motdepasseuser = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    livr_iban = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    livr_datedebutactivite = table.Column<DateOnly>(type: "date", nullable: true),
                    livr_notemoyenne = table.Column<decimal>(type: "numeric(2,1)", precision: 2, scale: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_livreur", x => x.livr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_pays_pys",
                columns: table => new
                {
                    pys_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('pays_id_seq'::regclass)"),
                    pys_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    pys_pourcentagetva = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pays", x => x.pys_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_produit_pdt",
                columns: table => new
                {
                    pdt_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('produit_id_seq'::regclass)"),
                    pdt_nom = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    pdt_prix = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    pdt_image = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    pdt_description = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produit", x => x.pdt_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_responsableenseigne_rse",
                columns: table => new
                {
                    rse_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('responsable_enseigne_id_seq'::regclass)"),
                    rse_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    rse_prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    rse_telephone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    rse_email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    rse_motdepasse = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_responsable_enseigne", x => x.rse_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_restaurateur_rst",
                columns: table => new
                {
                    rst_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('restaurateur_id_seq'::regclass)"),
                    rst_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    rst_prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    rst_telephone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    rst_email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    rst_motdepasse = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_restaurateur", x => x.rst_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeprestation_tpn",
                columns: table => new
                {
                    tpn_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('type_prestation_id_seq'::regclass)"),
                    tpn_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    tpn_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    tpn_image = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_type_prestation", x => x.tpn_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_codepostal_cp",
                columns: table => new
                {
                    cp_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('code_postal_id_seq'::regclass)"),
                    pys_id = table.Column<int>(type: "integer", nullable: true),
                    cp_cp = table.Column<string>(type: "character(5)", fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_code_postal", x => x.cp_id);
                    table.ForeignKey(
                        name: "fk_code_postal_pays",
                        column: x => x.pys_id,
                        principalTable: "t_e_pays_pys",
                        principalColumn: "pys_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_produitcategorie_pce",
                columns: table => new
                {
                    pdt_id = table.Column<int>(type: "integer", nullable: false),
                    cpt_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produit_categorie", x => new { x.pdt_id, x.cpt_id });
                    table.ForeignKey(
                        name: "fk_a3_categorie_produit",
                        column: x => x.cpt_id,
                        principalTable: "t_e_categorieproduit_cpt",
                        principalColumn: "cpt_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_a3_produit",
                        column: x => x.pdt_id,
                        principalTable: "t_e_produit_pdt",
                        principalColumn: "pdt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_ville_vil",
                columns: table => new
                {
                    vil_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('ville_id_seq'::regclass)"),
                    pys_id = table.Column<int>(type: "integer", nullable: true),
                    cp_id = table.Column<int>(type: "integer", nullable: true),
                    vil_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ville", x => x.vil_id);
                    table.ForeignKey(
                        name: "fk_ville_code_postal",
                        column: x => x.cp_id,
                        principalTable: "t_e_codepostal_cp",
                        principalColumn: "cp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ville_pays",
                        column: x => x.pys_id,
                        principalTable: "t_e_pays_pys",
                        principalColumn: "pys_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_adresse_adr",
                columns: table => new
                {
                    adr_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('adresse_id_seq'::regclass)"),
                    vil_id = table.Column<int>(type: "integer", nullable: true),
                    adr_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    adr_latitude = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    adr_longitude = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adresse", x => x.adr_id);
                    table.ForeignKey(
                        name: "fk_adresse_ville",
                        column: x => x.vil_id,
                        principalTable: "t_e_ville_vil",
                        principalColumn: "vil_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_entreprise_ent",
                columns: table => new
                {
                    ent_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('entreprise_id_seq'::regclass)"),
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    ent_siret = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ent_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ent_taille = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entreprise", x => x.ent_id);
                    table.ForeignKey(
                        name: "fk_entreprise_adresse",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_etablissement_etb",
                columns: table => new
                {
                    etb_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('etablissement_id_seq'::regclass)"),
                    rst_id = table.Column<int>(type: "integer", nullable: false),
                    etb_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    etb_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    etb_description = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: true),
                    etb_image = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    etb_livraison = table.Column<bool>(type: "boolean", nullable: true),
                    etb_aemporter = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_etablissement", x => x.etb_id);
                    table.ForeignKey(
                        name: "fk_etablissement_adresse",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_etablissement_restaurateur",
                        column: x => x.rst_id,
                        principalTable: "t_e_restaurateur_rst",
                        principalColumn: "rst_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_velo_vel",
                columns: table => new
                {
                    vel_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('velo_id_seq'::regclass)"),
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    vel_numero = table.Column<int>(type: "integer", nullable: false),
                    vel_estdisponible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_velo", x => x.vel_id);
                    table.ForeignKey(
                        name: "fk_velo_adresse",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_client_clt",
                columns: table => new
                {
                    clt_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('client_id_seq'::regclass)"),
                    ent_id = table.Column<int>(type: "integer", nullable: true),
                    adr_id = table.Column<int>(type: "integer", nullable: true),
                    clt_genre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    clt_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    clt_prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    clt_datenaissance = table.Column<DateOnly>(type: "date", nullable: false),
                    clt_telephone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    clt_email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    clt_motdepasse = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    clt_photoprofile = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    clt_souhaiterecevoirbonplan = table.Column<bool>(type: "boolean", nullable: true),
                    clt_mfaactivee = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    clt_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    clt_lastconnexion = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "now()"),
                    clt_demandesuppression = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client", x => x.clt_id);
                    table.ForeignKey(
                        name: "fk_client_adresse",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_client_entreprise",
                        column: x => x.ent_id,
                        principalTable: "t_e_entreprise_ent",
                        principalColumn: "ent_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_coursier_crr",
                columns: table => new
                {
                    crr_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('coursier_id_seq'::regclass)"),
                    ent_id = table.Column<int>(type: "integer", nullable: false),
                    adr_id = table.Column<int>(type: "integer", nullable: true),
                    crr_genreuser = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    crr_nomuser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    crr_prenomuser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    crr_datenaissance = table.Column<DateOnly>(type: "date", nullable: false),
                    crr_telephone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    crr_emailuser = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    crr_motdepasseuser = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    crr_numerocartevtc = table.Column<string>(type: "character(12)", fixedLength: true, maxLength: 12, nullable: false),
                    crr_iban = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    crr_datedebutactivite = table.Column<DateOnly>(type: "date", nullable: true),
                    crr_notemoyenne = table.Column<decimal>(type: "numeric(2,1)", precision: 2, scale: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coursier", x => x.crr_id);
                    table.ForeignKey(
                        name: "fk_coursier_adresse",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_coursier_entreprise",
                        column: x => x.ent_id,
                        principalTable: "t_e_entreprise_ent",
                        principalColumn: "ent_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_gestionetablissement_ges",
                columns: table => new
                {
                    ges_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('gestion_etablissement_id_seq'::regclass)"),
                    etb_id = table.Column<int>(type: "integer", nullable: false),
                    rse_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gestion_etablissement", x => x.ges_id);
                    table.ForeignKey(
                        name: "fk_gestion_etablissement",
                        column: x => x.etb_id,
                        principalTable: "t_e_etablissement_etb",
                        principalColumn: "etb_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gestion_responsable",
                        column: x => x.rse_id,
                        principalTable: "t_e_responsableenseigne_rse",
                        principalColumn: "rse_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_acommecategorie_acc",
                columns: table => new
                {
                    cpn_id = table.Column<int>(type: "integer", nullable: false),
                    etb_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_a_comme_categorie", x => new { x.cpn_id, x.etb_id });
                    table.ForeignKey(
                        name: "fk_a_comme_categorie_categorie",
                        column: x => x.cpn_id,
                        principalTable: "t_e_categorieprestation_cpr",
                        principalColumn: "cpr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_a_comme_categorie_etablissement",
                        column: x => x.etb_id,
                        principalTable: "t_e_etablissement_etb",
                        principalColumn: "etb_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_estsituea2_esa2",
                columns: table => new
                {
                    pdt_id = table.Column<int>(type: "integer", nullable: false),
                    etb_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_est_situe_a_2", x => new { x.pdt_id, x.etb_id });
                    table.ForeignKey(
                        name: "fk_est_situe2_etablissement",
                        column: x => x.etb_id,
                        principalTable: "t_e_etablissement_etb",
                        principalColumn: "etb_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_est_situe2_produit",
                        column: x => x.pdt_id,
                        principalTable: "t_e_produit_pdt",
                        principalColumn: "pdt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_lieufavori_lfs",
                columns: table => new
                {
                    lfs_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('lieu_favori_id_seq'::regclass)"),
                    clt_id = table.Column<int>(type: "integer", nullable: false),
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    lfs_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lieu_favori", x => x.lfs_id);
                    table.ForeignKey(
                        name: "fk_lieu_favori_adresse",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_lieu_favori_client",
                        column: x => x.clt_id,
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_otp_otp",
                columns: table => new
                {
                    otp_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('otp_id_seq'::regclass)"),
                    clt_id = table.Column<int>(type: "integer", nullable: false),
                    otp_code = table.Column<string>(type: "character(6)", fixedLength: true, maxLength: 6, nullable: false),
                    otp_dategeneration = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    otp_dateexpiration = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    otp_utilise = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_otp", x => x.otp_id);
                    table.ForeignKey(
                        name: "fk_otp_client",
                        column: x => x.clt_id,
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_panier_pnr",
                columns: table => new
                {
                    pnr_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('panier_id_seq'::regclass)"),
                    clt_id = table.Column<int>(type: "integer", nullable: false),
                    pnr_prix = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_panier", x => x.pnr_id);
                    table.ForeignKey(
                        name: "fk_panier_client",
                        column: x => x.clt_id,
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_reservation_res",
                columns: table => new
                {
                    res_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('reservation_id_seq'::regclass)"),
                    clt_id = table.Column<int>(type: "integer", nullable: false),
                    res_date = table.Column<DateOnly>(type: "date", nullable: true),
                    res_heure = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    res_pourqui = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reservation", x => x.res_id);
                    table.ForeignKey(
                        name: "fk_reservation_client",
                        column: x => x.clt_id,
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_clientcarte_clca",
                columns: table => new
                {
                    cb_id = table.Column<int>(type: "integer", nullable: false),
                    clt_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_carte", x => new { x.cb_id, x.clt_id });
                    table.ForeignKey(
                        name: "fk_appartient2_carte_bancaire",
                        column: x => x.cb_id,
                        principalTable: "t_e_cartebancaire_ctbr",
                        principalColumn: "cb_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_appartient2_client",
                        column: x => x.clt_id,
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_entretien_ett",
                columns: table => new
                {
                    ett_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('entretien_id_seq'::regclass)"),
                    crr_id = table.Column<int>(type: "integer", nullable: false),
                    ett_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ett_status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'En attente'::character varying"),
                    ett_resultat = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ett_rdvlogistiquedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ett_rdvlogistiquelieu = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entretien", x => x.ett_id);
                    table.ForeignKey(
                        name: "fk_entretien_coursier",
                        column: x => x.crr_id,
                        principalTable: "t_e_coursier_crr",
                        principalColumn: "crr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_horaires_hor",
                columns: table => new
                {
                    hor_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('horaires_id_seq'::regclass)"),
                    etb_id = table.Column<int>(type: "integer", nullable: true),
                    crr_id = table.Column<int>(type: "integer", nullable: true),
                    livr_id = table.Column<int>(type: "integer", nullable: true),
                    hor_joursemaine = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    hor_heuredebut = table.Column<DateTimeOffset>(type: "time with time zone", nullable: true),
                    hor_heurefin = table.Column<DateTimeOffset>(type: "time with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_horaires", x => x.hor_id);
                    table.ForeignKey(
                        name: "fk_horaires_coursier",
                        column: x => x.crr_id,
                        principalTable: "t_e_coursier_crr",
                        principalColumn: "crr_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_horaires_etablissement",
                        column: x => x.etb_id,
                        principalTable: "t_e_etablissement_etb",
                        principalColumn: "etb_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_horaires_livreur",
                        column: x => x.livr_id,
                        principalTable: "t_e_livreur_livr",
                        principalColumn: "livr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_reglementsalaire_rsa",
                columns: table => new
                {
                    rsa_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('reglement_salaire_id_seq'::regclass)"),
                    crr_id = table.Column<int>(type: "integer", nullable: true),
                    livr_id = table.Column<int>(type: "integer", nullable: true),
                    rsa_montantreglement = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reglement_salaire", x => x.rsa_id);
                    table.ForeignKey(
                        name: "fk_reglement_salaire_coursier",
                        column: x => x.crr_id,
                        principalTable: "t_e_coursier_crr",
                        principalColumn: "crr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_vehicule_vcl",
                columns: table => new
                {
                    vcl_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('vehicule_id_seq'::regclass)"),
                    crr_id = table.Column<int>(type: "integer", nullable: false),
                    vcl_immatriculation = table.Column<string>(type: "character(9)", fixedLength: true, maxLength: 9, nullable: false),
                    vcl_marque = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    vcl_modele = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    vcl_capacite = table.Column<int>(type: "integer", nullable: true),
                    vcl_accepteanimaux = table.Column<bool>(type: "boolean", nullable: false),
                    vcl_estelectrique = table.Column<bool>(type: "boolean", nullable: false),
                    vcl_estconfortable = table.Column<bool>(type: "boolean", nullable: false),
                    vcl_estrecent = table.Column<bool>(type: "boolean", nullable: false),
                    vcl_estluxueux = table.Column<bool>(type: "boolean", nullable: false),
                    vcl_couleur = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    vcl_statusprocessuslogistique = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'En attente'::character varying"),
                    vcl_demandemodification = table.Column<string>(type: "text", nullable: true),
                    vcl_demandemodificationeffectue = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehicule", x => x.vcl_id);
                    table.ForeignKey(
                        name: "fk_vehicule_coursier",
                        column: x => x.crr_id,
                        principalTable: "t_e_coursier_crr",
                        principalColumn: "crr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_commande_cmd",
                columns: table => new
                {
                    cmd_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('commande_id_seq'::regclass)"),
                    pnr_id = table.Column<int>(type: "integer", nullable: false),
                    livr_id = table.Column<int>(type: "integer", nullable: true),
                    cb_id = table.Column<int>(type: "integer", nullable: true),
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    cmd_prix = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    cmd_temps = table.Column<int>(type: "integer", nullable: false),
                    cmd_heurecreation = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    cmd_heure = table.Column<DateTime>(type: "date", nullable: false),
                    cmd_estlivraison = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    cmd_statut = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    cmd_refusdemandee = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    cmd_remboursementeffectue = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_commande", x => x.cmd_id);
                    table.ForeignKey(
                        name: "fk_commande_livreur",
                        column: x => x.livr_id,
                        principalTable: "t_e_livreur_livr",
                        principalColumn: "livr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_commande_panier",
                        column: x => x.pnr_id,
                        principalTable: "t_e_panier_pnr",
                        principalColumn: "pnr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_contient2_c2",
                columns: table => new
                {
                    pnr_id = table.Column<int>(type: "integer", nullable: false),
                    pdt_id = table.Column<int>(type: "integer", nullable: false),
                    etb_id = table.Column<int>(type: "integer", nullable: false),
                    c2_quantite = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contient_2", x => new { x.pnr_id, x.pdt_id });
                    table.ForeignKey(
                        name: "fk_contient2_etablissement",
                        column: x => x.etb_id,
                        principalTable: "t_e_etablissement_etb",
                        principalColumn: "etb_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contient2_panier",
                        column: x => x.pnr_id,
                        principalTable: "t_e_panier_pnr",
                        principalColumn: "pnr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contient2_produit",
                        column: x => x.pdt_id,
                        principalTable: "t_e_produit_pdt",
                        principalColumn: "pdt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_course_crs",
                columns: table => new
                {
                    crs_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('course_id_seq'::regclass)"),
                    crr_id = table.Column<int>(type: "integer", nullable: true),
                    cb_id = table.Column<int>(type: "integer", nullable: false),
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    res_id = table.Column<int>(type: "integer", nullable: false),
                    adr_adr_id = table.Column<int>(type: "integer", nullable: false),
                    tpn_id = table.Column<int>(type: "integer", nullable: false),
                    crs_date = table.Column<DateOnly>(type: "date", nullable: false),
                    crs_heure = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    crs_prix = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: false),
                    crs_statut = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    crs_note = table.Column<decimal>(type: "numeric(2,1)", precision: 2, scale: 1, nullable: true),
                    crs_commentaire = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: true),
                    crs_pourboire = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    crs_distance = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    crs_temps = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course", x => x.crs_id);
                    table.ForeignKey(
                        name: "fk_course_adresse_arrivee",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_course_adresse_depart",
                        column: x => x.adr_adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_course_carte_bancaire",
                        column: x => x.cb_id,
                        principalTable: "t_e_cartebancaire_ctbr",
                        principalColumn: "cb_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_course_coursier",
                        column: x => x.crr_id,
                        principalTable: "t_e_coursier_crr",
                        principalColumn: "crr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_course_reservation",
                        column: x => x.res_id,
                        principalTable: "t_e_reservation_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_course_type_prestation",
                        column: x => x.tpn_id,
                        principalTable: "t_e_typeprestation_tpn",
                        principalColumn: "tpn_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_veloreservation_velr",
                columns: table => new
                {
                    velr_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('velo_reservation_id_seq'::regclass)"),
                    vel_id = table.Column<int>(type: "integer", nullable: false),
                    velr_dureereservation = table.Column<int>(type: "integer", nullable: false),
                    velr_prixreservation = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_velo_reservation", x => new { x.velr_id, x.vel_id });
                    table.ForeignKey(
                        name: "fk_velo_reservation_reservation",
                        column: x => x.velr_id,
                        principalTable: "t_e_reservation_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_velo_reservation_velo",
                        column: x => x.vel_id,
                        principalTable: "t_e_velo_vel",
                        principalColumn: "vel_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_acommetype_act",
                columns: table => new
                {
                    vcl_id = table.Column<int>(type: "integer", nullable: false),
                    tpn_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_a_comme_type", x => new { x.vcl_id, x.tpn_id });
                    table.ForeignKey(
                        name: "fk_a_comme_type_type_prestation",
                        column: x => x.tpn_id,
                        principalTable: "t_e_typeprestation_tpn",
                        principalColumn: "tpn_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_a_comme_type_vehicule",
                        column: x => x.vcl_id,
                        principalTable: "t_e_vehicule_vcl",
                        principalColumn: "vcl_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_facture_fac",
                columns: table => new
                {
                    fac_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('facture_id_seq'::regclass)"),
                    res_id = table.Column<int>(type: "integer", nullable: true),
                    cmd_id = table.Column<int>(type: "integer", nullable: true),
                    pys_id = table.Column<int>(type: "integer", nullable: false),
                    clt_id = table.Column<int>(type: "integer", nullable: false),
                    fac_montantreglement = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    fac_datefacture = table.Column<DateOnly>(type: "date", nullable: true),
                    fac_quantite = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_facture", x => x.fac_id);
                    table.ForeignKey(
                        name: "fk_facture_client",
                        column: x => x.clt_id,
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_facture_commande",
                        column: x => x.cmd_id,
                        principalTable: "t_e_commande_cmd",
                        principalColumn: "cmd_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_facture_pays",
                        column: x => x.pys_id,
                        principalTable: "t_e_pays_pys",
                        principalColumn: "pys_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_facture_reservation",
                        column: x => x.res_id,
                        principalTable: "t_e_reservation_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_vil_id",
                table: "t_e_adresse_adr",
                column: "vil_id");

            migrationBuilder.CreateIndex(
                name: "carte_bancaire_numerocb_key",
                table: "t_e_cartebancaire_ctbr",
                column: "cb_numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_clt_adr_id",
                table: "t_e_client_clt",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_clt_ent_id",
                table: "t_e_client_clt",
                column: "ent_id");

            migrationBuilder.CreateIndex(
                name: "uq_client_mail",
                table: "t_e_client_clt",
                column: "clt_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_codepostal_cp_pys_id",
                table: "t_e_codepostal_cp",
                column: "pys_id");

            migrationBuilder.CreateIndex(
                name: "uq_cp",
                table: "t_e_codepostal_cp",
                column: "cp_cp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_livr_id",
                table: "t_e_commande_cmd",
                column: "livr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_pnr_id",
                table: "t_e_commande_cmd",
                column: "pnr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_course_crs_adr_adr_id",
                table: "t_e_course_crs",
                column: "adr_adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_course_crs_adr_id",
                table: "t_e_course_crs",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_course_crs_cb_id",
                table: "t_e_course_crs",
                column: "cb_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_course_crs_crr_id",
                table: "t_e_course_crs",
                column: "crr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_course_crs_res_id",
                table: "t_e_course_crs",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_course_crs_tpn_id",
                table: "t_e_course_crs",
                column: "tpn_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_coursier_crr_adr_id",
                table: "t_e_coursier_crr",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_coursier_crr_ent_id",
                table: "t_e_coursier_crr",
                column: "ent_id");

            migrationBuilder.CreateIndex(
                name: "uq_coursier_iban",
                table: "t_e_coursier_crr",
                column: "crr_iban",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_coursier_mail",
                table: "t_e_coursier_crr",
                column: "crr_emailuser",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_coursier_numcarte",
                table: "t_e_coursier_crr",
                column: "crr_numerocartevtc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_entreprise_ent_adr_id",
                table: "t_e_entreprise_ent",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_entretien_ett_crr_id",
                table: "t_e_entretien_ett",
                column: "crr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_etablissement_etb_adr_id",
                table: "t_e_etablissement_etb",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_etablissement_etb_rst_id",
                table: "t_e_etablissement_etb",
                column: "rst_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_facture_fac_clt_id",
                table: "t_e_facture_fac",
                column: "clt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_facture_fac_cmd_id",
                table: "t_e_facture_fac",
                column: "cmd_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_facture_fac_pys_id",
                table: "t_e_facture_fac",
                column: "pys_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_facture_fac_res_id",
                table: "t_e_facture_fac",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_gestionetablissement_ges_etb_id",
                table: "t_e_gestionetablissement_ges",
                column: "etb_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_gestionetablissement_ges_rse_id",
                table: "t_e_gestionetablissement_ges",
                column: "rse_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_horaires_hor_crr_id",
                table: "t_e_horaires_hor",
                column: "crr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_horaires_hor_etb_id",
                table: "t_e_horaires_hor",
                column: "etb_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_horaires_hor_livr_id",
                table: "t_e_horaires_hor",
                column: "livr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_lieufavori_lfs_adr_id",
                table: "t_e_lieufavori_lfs",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_lieufavori_lfs_clt_id",
                table: "t_e_lieufavori_lfs",
                column: "clt_id");

            migrationBuilder.CreateIndex(
                name: "uq_livreur_iban",
                table: "t_e_livreur_livr",
                column: "livr_iban",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_livreur_mail",
                table: "t_e_livreur_livr",
                column: "livr_emailuser",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_otp_otp_clt_id",
                table: "t_e_otp_otp",
                column: "clt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_panier_pnr_clt_id",
                table: "t_e_panier_pnr",
                column: "clt_id");

            migrationBuilder.CreateIndex(
                name: "uq_nompays",
                table: "t_e_pays_pys",
                column: "pys_nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_reglementsalaire_rsa_crr_id",
                table: "t_e_reglementsalaire_rsa",
                column: "crr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_reservation_res_clt_id",
                table: "t_e_reservation_res",
                column: "clt_id");

            migrationBuilder.CreateIndex(
                name: "uq_responsable_email",
                table: "t_e_responsableenseigne_rse",
                column: "rse_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_restaurateur_mail",
                table: "t_e_restaurateur_rst",
                column: "rst_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_vehicule_vcl_crr_id",
                table: "t_e_vehicule_vcl",
                column: "crr_id");

            migrationBuilder.CreateIndex(
                name: "uq_vehicule_immatriculation",
                table: "t_e_vehicule_vcl",
                column: "vcl_immatriculation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_velo_vel_adr_id",
                table: "t_e_velo_vel",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "uq_velo_numero",
                table: "t_e_velo_vel",
                column: "vel_numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_veloreservation_velr_vel_id",
                table: "t_e_veloreservation_velr",
                column: "vel_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_ville_vil_cp_id",
                table: "t_e_ville_vil",
                column: "cp_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_ville_vil_pys_id",
                table: "t_e_ville_vil",
                column: "pys_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_acommecategorie_acc_etb_id",
                table: "t_j_acommecategorie_acc",
                column: "etb_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_acommetype_act_tpn_id",
                table: "t_j_acommetype_act",
                column: "tpn_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_clientcarte_clca_clt_id",
                table: "t_j_clientcarte_clca",
                column: "clt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_contient2_c2_etb_id",
                table: "t_j_contient2_c2",
                column: "etb_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_contient2_c2_pdt_id",
                table: "t_j_contient2_c2",
                column: "pdt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_estsituea2_esa2_etb_id",
                table: "t_j_estsituea2_esa2",
                column: "etb_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_produitcategorie_pce_cpt_id",
                table: "t_j_produitcategorie_pce",
                column: "cpt_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_course_crs");

            migrationBuilder.DropTable(
                name: "t_e_entretien_ett");

            migrationBuilder.DropTable(
                name: "t_e_facture_fac");

            migrationBuilder.DropTable(
                name: "t_e_gestionetablissement_ges");

            migrationBuilder.DropTable(
                name: "t_e_horaires_hor");

            migrationBuilder.DropTable(
                name: "t_e_lieufavori_lfs");

            migrationBuilder.DropTable(
                name: "t_e_otp_otp");

            migrationBuilder.DropTable(
                name: "t_e_reglementsalaire_rsa");

            migrationBuilder.DropTable(
                name: "t_e_veloreservation_velr");

            migrationBuilder.DropTable(
                name: "t_j_acommecategorie_acc");

            migrationBuilder.DropTable(
                name: "t_j_acommetype_act");

            migrationBuilder.DropTable(
                name: "t_j_clientcarte_clca");

            migrationBuilder.DropTable(
                name: "t_j_contient2_c2");

            migrationBuilder.DropTable(
                name: "t_j_estsituea2_esa2");

            migrationBuilder.DropTable(
                name: "t_j_produitcategorie_pce");

            migrationBuilder.DropTable(
                name: "t_e_commande_cmd");

            migrationBuilder.DropTable(
                name: "t_e_responsableenseigne_rse");

            migrationBuilder.DropTable(
                name: "t_e_reservation_res");

            migrationBuilder.DropTable(
                name: "t_e_velo_vel");

            migrationBuilder.DropTable(
                name: "t_e_categorieprestation_cpr");

            migrationBuilder.DropTable(
                name: "t_e_typeprestation_tpn");

            migrationBuilder.DropTable(
                name: "t_e_vehicule_vcl");

            migrationBuilder.DropTable(
                name: "t_e_cartebancaire_ctbr");

            migrationBuilder.DropTable(
                name: "t_e_etablissement_etb");

            migrationBuilder.DropTable(
                name: "t_e_categorieproduit_cpt");

            migrationBuilder.DropTable(
                name: "t_e_produit_pdt");

            migrationBuilder.DropTable(
                name: "t_e_livreur_livr");

            migrationBuilder.DropTable(
                name: "t_e_panier_pnr");

            migrationBuilder.DropTable(
                name: "t_e_coursier_crr");

            migrationBuilder.DropTable(
                name: "t_e_restaurateur_rst");

            migrationBuilder.DropTable(
                name: "t_e_client_clt");

            migrationBuilder.DropTable(
                name: "t_e_entreprise_ent");

            migrationBuilder.DropTable(
                name: "t_e_adresse_adr");

            migrationBuilder.DropTable(
                name: "t_e_ville_vil");

            migrationBuilder.DropTable(
                name: "t_e_codepostal_cp");

            migrationBuilder.DropTable(
                name: "t_e_pays_pys");

            migrationBuilder.DropSequence(
                name: "adresse_id_seq");

            migrationBuilder.DropSequence(
                name: "carte_bancaire_id_seq");

            migrationBuilder.DropSequence(
                name: "categorie_prestation_id_seq");

            migrationBuilder.DropSequence(
                name: "categorie_produit_id_seq");

            migrationBuilder.DropSequence(
                name: "client_id_seq");

            migrationBuilder.DropSequence(
                name: "code_postal_id_seq");

            migrationBuilder.DropSequence(
                name: "commande_id_seq");

            migrationBuilder.DropSequence(
                name: "course_id_seq");

            migrationBuilder.DropSequence(
                name: "coursier_id_seq");

            migrationBuilder.DropSequence(
                name: "entreprise_id_seq");

            migrationBuilder.DropSequence(
                name: "entretien_id_seq");

            migrationBuilder.DropSequence(
                name: "etablissement_id_seq");

            migrationBuilder.DropSequence(
                name: "facture_id_seq");

            migrationBuilder.DropSequence(
                name: "gestion_etablissement_id_seq");

            migrationBuilder.DropSequence(
                name: "horaires_id_seq");

            migrationBuilder.DropSequence(
                name: "lieu_favori_id_seq");

            migrationBuilder.DropSequence(
                name: "livreur_id_seq");

            migrationBuilder.DropSequence(
                name: "otp_id_seq");

            migrationBuilder.DropSequence(
                name: "panier_id_seq");

            migrationBuilder.DropSequence(
                name: "pays_id_seq");

            migrationBuilder.DropSequence(
                name: "planning_reservation_id_seq");

            migrationBuilder.DropSequence(
                name: "produit_id_seq");

            migrationBuilder.DropSequence(
                name: "reglement_salaire_id_seq");

            migrationBuilder.DropSequence(
                name: "reservation_id_seq");

            migrationBuilder.DropSequence(
                name: "responsable_enseigne_id_seq");

            migrationBuilder.DropSequence(
                name: "restaurateur_id_seq");

            migrationBuilder.DropSequence(
                name: "type_prestation_id_seq");

            migrationBuilder.DropSequence(
                name: "vehicule_id_seq");

            migrationBuilder.DropSequence(
                name: "velo_id_seq");

            migrationBuilder.DropSequence(
                name: "velo_reservation_id_seq");

            migrationBuilder.DropSequence(
                name: "ville_id_seq");
        }
    }
}
