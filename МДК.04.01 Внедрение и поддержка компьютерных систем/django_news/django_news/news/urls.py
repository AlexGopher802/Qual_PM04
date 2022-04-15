from django.urls import path, include
from . import views

urlpatterns = [
    path('', views.index, name='index_url'),
    path('<int:post_id>/', views.post_detail, name='post_detail_url'),
    path('<int:post_id>/add_comment', views.add_comment, name='add_comment_url'),
    path('post_create', views.PostCreateview.as_view(), name='post_create_url'),
    path('<int:id_post>/post_update/', views.PostUpdate.as_view(), name='post_update_url'),
    path('<int:id_post>/post_delete/', views.PostDelete.as_view(), name='post_delete_url'),
    path('account/', views.UserUpdateView.as_view(), name='profile_detail_url'),
    path('account/update', views.UserUpdateView.as_view(), name='profile_update_url'),
    path('account/posts', views.UserPostListView.as_view(), name='user_posts_url'),
]