const readJson = (id) => {
	const element = document.getElementById(id);
	if (!element) return null;
	try {
		return JSON.parse(element.textContent || "");
	} catch {
		return null;
	}
};

const rawProducts = readJson("productData") ?? [];
const rawFeatured = readJson("featuredData") ?? [];
const rawCategories = readJson("categoryData") ?? [];

const productData = rawProducts.map((item) => ({
	id: item.id,
	name: item.name,
	category: item.categoryName || "Uncategorized",
	price: item.price ?? 0,
	description: item.description || "",
	imageUrls: item.imageUrls || [],
	createdAt: item.createdAt,
	updatedAt: item.updatedAt,
}));

const featuredSource = rawFeatured.length > 0 ? rawFeatured : rawProducts.slice(0, 4);
const featuredData = featuredSource.map((item) => ({
	id: item.id,
	name: item.name,
	category: item.categoryName || "Uncategorized",
	price: item.price ?? 0,
	description: item.description || "",
}));

const categories = [
	"All",
	...rawCategories.map((category) => category.name).filter(Boolean),
];

const categoryMenu = document.getElementById("layoutCategoryMenu");
const categoryFilters = document.getElementById("categoryFilters");
const featuredGrid = document.getElementById("featuredGrid");
const productGrid = document.getElementById("productGrid");
const detailCard = document.getElementById("detailCard");

const state = {
	activeCategory: "All",
};

const formatPrice = (value) => `$${Number(value).toFixed(0)}`;
const formatDate = (value) => {
	if (!value) return "-";
	const date = new Date(value);
	if (Number.isNaN(date.getTime())) return "-";
	return date.toLocaleDateString("en-US", {
		year: "numeric",
		month: "short",
		day: "numeric",
	});
};

const renderPills = (container, onClick) => {
	if (!container) return;
	container.innerHTML = "";
	categories.forEach((category) => {
		const pill = document.createElement("button");
		pill.type = "button";
		pill.className = `pill${category === state.activeCategory ? " active" : ""}`;
		pill.textContent = category;
		pill.addEventListener("click", () => onClick(category));
		container.appendChild(pill);
	});
};

const renderFeatured = () => {
	if (!featuredGrid) return;
	featuredGrid.innerHTML = "";
	const source = featuredData.length > 0 ? featuredData : productData.slice(0, 4);
	if (source.length === 0) {
		featuredGrid.innerHTML = "<div class=\"empty-state\">No featured products yet.</div>";
		return;
	}
	source.forEach((item) => {
		const card = document.createElement("div");
		card.className = "featured-card";
		card.innerHTML = `
			<span class="tag">Top pick</span>
			<h3>${item.name}</h3>
			<p>${item.description}</p>
			<div class="price">${formatPrice(item.price)}</div>
		`;
		featuredGrid.appendChild(card);
	});
};

const renderProducts = () => {
	if (!productGrid) return;
	productGrid.innerHTML = "";
	const filtered =
		state.activeCategory === "All"
			? productData
			: productData.filter((item) => item.category === state.activeCategory);

	if (filtered.length === 0) {
		productGrid.innerHTML = "<div class=\"empty-state\">No products in this category.</div>";
		return;
	}

	filtered.forEach((item) => {
		const card = document.createElement("article");
		card.className = "product-card";
		card.innerHTML = `
			<div class="category">${item.category}</div>
			<h4>${item.name}</h4>
			<p>${item.description}</p>
			<div class="price">${formatPrice(item.price)}</div>
		`;
		card.addEventListener("click", () => renderDetail(item));
		productGrid.appendChild(card);
	});
};

const renderDetail = (item) => {
	if (!detailCard) return;
	const specs = {
		Category: item.category,
		"Created": formatDate(item.createdAt),
		"Updated": formatDate(item.updatedAt),
		"Images": item.imageUrls?.length ?? 0,
	};
	const specRows = Object.entries(specs)
		.map(
			([label, value]) => `
				<li class="spec-item">
					${label}
					<span>${value}</span>
				</li>
			`
		)
		.join("");

	detailCard.innerHTML = `
		<p class="eyebrow">Product detail</p>
		<h3 class="detail-title">${item.name}</h3>
		<p>${item.description}</p>
		<div class="price">${formatPrice(item.price)}</div>
		<ul class="spec-list">${specRows}</ul>
		<button class="pill-button" type="button">Add to cart</button>
	`;
	detailCard.scrollIntoView({ behavior: "smooth", block: "nearest" });
};

const setCategory = (category) => {
	state.activeCategory = category;
	renderPills(categoryMenu, setCategory);
	renderPills(categoryFilters, setCategory);
	renderProducts();
};

document.addEventListener("DOMContentLoaded", () => {
	renderPills(categoryMenu, setCategory);
	renderPills(categoryFilters, setCategory);
	renderFeatured();
	renderProducts();
});
